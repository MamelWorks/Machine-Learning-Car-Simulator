
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;


public class NeuralNetwork : IComparable<NeuralNetwork>
{
    public int[] Layers; 
    public float[][] Neurons; 
    public float[][][] Weights; 

    public string Name;
    public string GenerationCode;
    public int MutationOfWhichPlace;
    public NeuralNetwork ParentNetwork;

    public float fitness;

    public NeuralNetwork(int[] layers)
    {
        Name = GameObject.Find("Manager").GetComponent<RandomNameGenerator>().GetRandomName();
        GenerationCode = "";
        this.Layers = new int[layers.Length];
        for (int i = 0; i < layers.Length; i++)
        {
            this.Layers[i] = layers[i];
        }
        MutationOfWhichPlace = 0;
        InitNeurons();
        InitWeights();
    }

    public NeuralNetwork(NeuralNetwork copyNetwork, int numberOfCopy)
    {
        MutationOfWhichPlace = 0;
        Name = GameObject.Find("Manager").GetComponent<RandomNameGenerator>().GetRandomName(); ;
        GenerationCode = copyNetwork.GenerationCode + numberOfCopy+',';
        ParentNetwork = copyNetwork;
        this.Layers = new int[copyNetwork.Layers.Length];
        for (int i = 0; i < copyNetwork.Layers.Length; i++)
        {
            this.Layers[i] = copyNetwork.Layers[i];
        }
        InitNeurons();
        InitWeights();
        CopyWeights(copyNetwork.Weights);
    }

    private void CopyWeights(float[][][] copyWeights)
    {
        for (int i = 0; i < Weights.Length; i++)
        {
            for (int j = 0; j < Weights[i].Length; j++)
            {
                for (int k = 0; k < Weights[i][j].Length; k++)
                {
                    Weights[i][j][k] = copyWeights[i][j][k];
                }
            }
        }
    }

    private void InitNeurons()
    {
        Neurons = Layers.Select(t => new float[t]).ToArray(); 
    }

    private void InitWeights()
    {
        List<float[][]> weightsList = new List<float[][]>();
        for (int i = 1; i < Layers.Length; i++)
        {
            List<float[]> layerWeightsList = new List<float[]>(); 
            int neuronsInPreviousLayer = Layers[i - 1];
            for (int j = 0; j < Neurons[i].Length; j++)
            {
                float[] neuronWeights = new float[neuronsInPreviousLayer];
                for (int k = 0; k < neuronsInPreviousLayer; k++)
                {
                    neuronWeights[k] = UnityEngine.Random.Range(-0.5f, 0.5f);
                }
                layerWeightsList.Add(neuronWeights); 
            }
            weightsList.Add(layerWeightsList.ToArray()); 
        }
        Weights = weightsList.ToArray();
    }

    public float[] FeedForward(float[] inputs)
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            Neurons[0][i] = inputs[i];
        }
        for (int i = 1; i < Layers.Length; i++)
        {
            for (int j = 0; j < Neurons[i].Length; j++)
            {
                float value = 0f;

                for (int k = 0; k < Neurons[i - 1].Length; k++)
                {
                    value += Weights[i - 1][j][k] * Neurons[i - 1][k];
                }
                Neurons[i][j] = (float)Math.Tanh(value); 
            }
        }
        return Neurons[Neurons.Length - 1];
    }

    public void Mutate()
    {
        for (int i = 0; i < Weights.Length; i++)
        {
            for (int j = 0; j < Weights[i].Length; j++)
            {
                for (int k = 0; k < Weights[i][j].Length; k++)
                {
                    float weight = Weights[i][j][k];

                    float randomNumber = UnityEngine.Random.Range(0f, 100f);
                    if (randomNumber <= 2f)
                    { 
                        weight *= -1f;
                    }
                    else if (randomNumber <= 4f)
                    { 
                        weight = UnityEngine.Random.Range(-0.5f, 0.5f);
                    }
                    else if (randomNumber <= 6f)
                    { 
                        float factor = UnityEngine.Random.Range(0f, 1f) + 1f;
                        weight *= factor;
                    }
                    else if (randomNumber <= 8f)
                    { 
                        float factor = UnityEngine.Random.Range(0f, 1f);
                        weight *= factor;
                    }
                    Weights[i][j][k] = weight;
                }
            }
        }
    }

    public void UpdateFitness(float fit)
    {
        fitness = fit;
    }

    public void SetFitness(float fit)
    {
        fitness = fit;

    }

    public int CompareTo(NeuralNetwork other)
    {
        if (other == null) return 1;

        if (fitness > other.fitness)
            return 1;
        else if (fitness < other.fitness)
            return -1;
        else
            return 0;
    }
}
