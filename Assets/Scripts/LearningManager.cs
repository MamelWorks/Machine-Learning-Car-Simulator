using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets;
using ChartAndGraph;

using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.UI;
using Random = System.Random;

public class LearningManager : MonoBehaviour
{
    public GameObject CarPrefab;
    public CurveImplementation Road;
    public Toggle AutoNextGen;
    public Toggle NextGenWhenNoCarLeft;
    public Toggle DeleteCarWithoutProgress;
    public Toggle FrontLeftToggle;
    public Toggle FrontRightToggle;
    public Toggle FrontToggle;
    public Toggle LeftToggle;
    public Toggle NextGenOnLap;
    public Toggle RightToggle;
    public Toggle AutoCleanToggle;
    public Slider AutoNextGenTimer;
    public Slider NumberOfHiddenLayerSlider;
    public Slider NeuronPerHiddenLayerSlider;
    public Slider DeleteCarWithoutProgressTimer;
    public Slider NumberOfCarSlider;
    public Slider NextGenOnLapSlider;
    public PlayerRacingManager PlayerRacingManager;
    public GraphChartBase GraphChartBase;
    public GameObject LearningCanvasGameObject;
    public GameObject RaceCanvasGameObject;
    public GameObject CarSelectorGameObject;

    public bool IsRacing;
    public float GenerationTime;
    public float GenerationTimeStart;
    public float NetworkTimeStart;
    public bool IsTraning = false;
    public int GenerationNumber = 0;
    public float BestLapTime { get; set; }
    public int MostLapCount { get; set; }
    public List<Car> SortedCars = null;
    public bool[] EnabledRaycasts;
    public List<string> Logs;


    private Car _bestCar;
    private List<Toggle> _rayCastList;
    private List<NeuralNetwork> _nets;
    private int _populationSize = 1;
    private List<Car> _carList;
    private Car _firstCar;

    void Start()
    {
        Logs = new List<string>();


        _rayCastList = new List<Toggle>()
        {
            FrontLeftToggle,
            FrontRightToggle,
            FrontToggle,
            LeftToggle,
            RightToggle
        };
        InitCars();

    }

    void Update()
    {
        if (IsRacing && IsTraning)
        {
            IsTraning = false;
            SetupRace();
            return;
        }

        if (IsRacing)
            return;

        if (IsTraning == false)
            NextGen();

        GenerationTime = Time.time - GenerationTimeStart;
        if (DeleteCarWithoutProgress.isOn)
        {
            foreach (var car in _carList.Where(x => x.gameObject.activeSelf))
            {
                if (car.TimeSinceLastFitnessUpdate > DeleteCarWithoutProgressTimer.value)
                {
                    car.CarCrashed = true;
                }
            }
        }



        var bestCar = _carList.OrderBy(x => x.BestLapTime).FirstOrDefault(x => x.BestLapTime != -1);
        if (bestCar == null)
        {
            BestLapTime = 0;
            MostLapCount = 0;
        }
        else
        {
            BestLapTime = bestCar.BestLapTime;
            MostLapCount = bestCar.LapCount;
            if (_bestCar == null)
            {
                Log(bestCar.Net.Name + " won first lap", true);
                GraphChartBase.DataSource.StartBatch();
                GraphChartBase.DataSource.AddPointToCategory("LapTime", GenerationNumber, GenerationTime);
                GraphChartBase.DataSource.EndBatch();
            }
            _bestCar = bestCar;
        }


        SortedCars = _carList.OrderByDescending(x => x.Fitness).ToList();


        if (_carList.All(x => x.CarCrashed) && NextGenWhenNoCarLeft.isOn)
        {
            IsTraning = false;
        }

        if (!NextGenOnLap.isOn)
            return;
        if (NextGenOnLapSlider.value == MostLapCount)
            IsTraning = false;

    }

    private void SetupRace()
    {
        foreach (var car in _carList)
        {
            Destroy(car.gameObject);
        }
        var opponentCar = CreateCarWithNetwork(GameObject.Find("CarSelector").GetComponent<CarSelector>().SelectedCarGameObject.GetComponent<Car>().Net);
        opponentCar.GetComponent<BoxCollider>().isTrigger = false;
        opponentCar.transform.position += Vector3.left;
        var playerCar = CreatePlayerCar();
        playerCar.transform.position += Vector3.right;
        playerCar.GetComponent<BoxCollider>().isTrigger = false;
        playerCar.GetComponent<Rigidbody>().drag = 2.6f;
        GameObject.Find("CameraManager").GetComponent<CameraManager>().Follow(playerCar);
        ChangeUiToLearningUi(false);
        Time.timeScale = 1;

        GameObject.Find("Speedometer").GetComponent<Speedometer>().PlayerCar = playerCar;
        _carList = new List<Car>
        {
            opponentCar.GetComponent<Car>(),
            playerCar.GetComponent<Car>()
        };

    }

    public void EndRace()
    {
        foreach (var car in _carList)
        {
            Destroy(car.gameObject);
        }

        ChangeUiToLearningUi(true);
        IsRacing = false;

    }

    private void ChangeUiToLearningUi(bool b)
    {
        CarSelectorGameObject.SetActive(b);
        LearningCanvasGameObject.SetActive(b);
        RaceCanvasGameObject.SetActive(!b);

    }

    private GameObject CreatePlayerCar()
    {
        GameObject playerCar = InstantiateCarPrefab();
        playerCar.GetComponent<Car>().IsPlayer = true;
        return playerCar;
    }


    public Car GetCarOnFirstPlace()
    {
        return _carList.Where(x => x.gameObject.activeSelf).OrderByDescending(x => x.Fitness)
                    .ThenByDescending(x => x.TimeSinceLastFitnessUpdate).First();
    }

    private void CreateCars()
    {
        if (_carList != null)
        {
            foreach (var car in _carList)
            {
                Destroy(car.gameObject);
            }
        }

        _carList = new List<Car>();

        for (int i = 0; i < _populationSize; i++)
        {
            CreateCarWithNetwork(_nets[i]);
        }
        GenerationTimeStart = Time.time;

    }

    private GameObject CreateCarWithNetwork(NeuralNetwork net)
    {
        Car car = InstantiateCarPrefab().GetComponent<Car>();
        car.Init(net);
        _carList.Add(car);

        for (int i = 0; i < EnabledRaycasts.Length; i++)
            car.transform.GetChild(i).gameObject.SetActive(EnabledRaycasts[i]);

        return car.gameObject;
    }

    private GameObject InstantiateCarPrefab()
    {
        return ((GameObject)Instantiate(CarPrefab, new Vector3(Road.waypoints[0].x, 5.5f, Road.waypoints[0].z), CarPrefab.transform.rotation));
    }

    public int[] GetLayers()
    {
        int numberOfHiddenLayers = Int32.Parse(NumberOfHiddenLayerSlider.value.ToString());
        int[] temp = new int[2 + numberOfHiddenLayers];
        temp[0] = _rayCastList.Count(x => x.isOn);
        for (int i = 1; i < numberOfHiddenLayers + 1; i++)
        {
            temp[i] = Int32.Parse(NeuronPerHiddenLayerSlider.value.ToString());
        }
        temp[temp.Length - 1] = 2;
        return temp;
    }
    public void InitCars()
    {
        SetRayCasts();
        _populationSize = Int32.Parse(NumberOfCarSlider.value.ToString());
        _nets = new List<NeuralNetwork>();
        GenerationNumber = 0;
        for (int i = 0; i < _populationSize; i++)
        {

            NeuralNetwork net = new NeuralNetwork(GetLayers());
            net.Mutate();
            _nets.Add(net);
        }
        NetworkTimeStart = Time.time;
        if (AutoCleanToggle.isOn)
            Logs = new List<string>();
        Log("New neural network generated", false);
        GraphChartBase.DataSource.StartBatch();
        GraphChartBase.DataSource.ClearCategory("LapTime");
        GraphChartBase.DataSource.EndBatch();
    }

    private void SetRayCasts()
    {
        EnabledRaycasts = new[] { false, false, false, false, false };
        EnabledRaycasts[0] = FrontLeftToggle.isOn;
        EnabledRaycasts[1] = FrontRightToggle.isOn;
        EnabledRaycasts[2] = FrontToggle.isOn;
        EnabledRaycasts[3] = RightToggle.isOn;
        EnabledRaycasts[4] = LeftToggle.isOn;
    }

    void InitCarNeuralNetworks()
    {
        _nets = new List<NeuralNetwork>();
        for (int i = 0; i < _populationSize; i++)
        {
            NeuralNetwork net = new NeuralNetwork(GetLayers());
            net.Mutate();
            _nets.Add(net);
        }
    }

    public void NextGen()
    {
        CancelInvoke();
        _bestCar = null;
        if (GenerationNumber == 0)
        {
            InitCarNeuralNetworks();
        }
        else
        {
            _nets.Sort();

            for (int i = 0; i < _populationSize; i++)
                _nets[i].MutationOfWhichPlace = 0;

            if (BestLapTime == 0)
            {
                for (int j = 0; j < 2; j++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        _nets[i + j * 3] = new NeuralNetwork(_nets[_populationSize - i - 1], i);
                        _nets[i + j * 3].Mutate();
                        _nets[i + j * 3].MutationOfWhichPlace = i + 1;
                    }
                }
                for (int i = 6 ; i < _populationSize - 6; i++)
                {
                    _nets[i] = new NeuralNetwork(GetLayers());
                    _nets[i].Mutate();
                }
            }
            else
            {
                for (int i = 0; i < _populationSize / 3; i++)
                {
                    _nets[i] = new NeuralNetwork(_nets[_populationSize - 1], i);
                    _nets[i].Mutate();
                    _nets[i].MutationOfWhichPlace = 1;
                }
                for (int i = _populationSize / 3; i < _populationSize / 3 * 2; i++)
                {
                    _nets[i] = new NeuralNetwork(_nets[_populationSize - 2], i);
                    _nets[i].Mutate();
                    _nets[i].MutationOfWhichPlace = 2;
                }
                for (int i = (_populationSize / 3) * 2; i < _populationSize / 3 * 3 -1; i++)
                {
                    _nets[i] = new NeuralNetwork(_nets[_populationSize - 3], i);
                    _nets[i].Mutate();
                    _nets[i].MutationOfWhichPlace = 3;
                }
            }

            for (int i = 1; i <= 3; i++)
            {
                _nets[_nets.Count - i].MutationOfWhichPlace = -1;
            }
            for (int i = 0; i < _populationSize; i++)
            {
                _nets[i].SetFitness(0f);
            }
        }


        GenerationNumber++;
        Log("Started generation number: " + GenerationNumber, false);
        IsTraning = true;
        CreateCars();
        if (AutoNextGen.isOn)
            Invoke("NextGen", AutoNextGenTimer.value);
    }

    public void Log(string text, bool generationLog)
    {
        string newText = "";

        if (generationLog)
            newText = "  " + MamelsHelpMethods.FloatToTime(GenerationTime, "0:00.00") + ": ";
        else
            newText = "<b>" + MamelsHelpMethods.FloatToTime(Time.time - NetworkTimeStart, "0:00.00") + ": </b> ";
        newText += text;
        Logs.Add(newText);
    }

    public void StartRace()
    {
        IsRacing = true;
    }
}
