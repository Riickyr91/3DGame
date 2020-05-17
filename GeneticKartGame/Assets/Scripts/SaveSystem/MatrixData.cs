using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;

[System.Serializable]
public class MatrixData
{
    public int nRows;
    public int nColumns;
    public float[] values;

    public MatrixData(Matrix<float> matrix)
    {
        Matrix<float> matrix2 = matrix;
        nRows = matrix.RowCount;
        nColumns = matrix.ColumnCount;
        values = new float[nRows * nColumns];

        int indexValues = 0;
        for(int i = 0; i < nRows; i++)
        {
            for(int j = 0; j < nColumns; j++)
            {
                values[indexValues] = matrix[i, j];
                indexValues++;
            }
        }
    }

    public Matrix<float> GetMatrix()
    {
        Matrix<float> matrix = Matrix<float>.Build.Dense(nRows, nColumns);

        int indexValues = 0;
        for(int i = 0; i < nRows; i++)
        {
            for(int j = 0; j < nColumns; j++)
            {
                matrix[i, j] = values[indexValues];
                indexValues++;
            }
        }

        return matrix;
    }

    public string ToString()
    {
        Matrix<float> matrix = GetMatrix();
        string output = "[";

        for(int i = 0; i < matrix.RowCount; i++)
        {
            output += " [";
            for(int j = 0; j < matrix.ColumnCount; j++)
            {
                output += matrix[i, j].ToString();
                if (j < matrix.ColumnCount - 1) output += ", ";
            }
            output += "] \n";
        }

        output += "]";
        return output;
    }
}
