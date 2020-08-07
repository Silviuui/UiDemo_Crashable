using System;
using System.Windows;

public class CrashSimulator
{
    private int _threshold;
    
    private Random _randomizer = new Random();

    public CrashSimulator(int threshold)
	{
        _threshold = ConvertThresholdToPercentage(threshold);
	}

    public void CrashIfItsTheTime()
    {
        if (_randomizer.Next(100) < _threshold)
            GenerateCrash();
    }

    private void GenerateCrash()
    {
        // Crash the application.
        throw new Exception("Simulated Crash!!!");
    }

    /// <summary>
    /// Map the Crash Slider values to percentage for crash probability.
    /// </summary>
    /// <param name="threshold">Slider position</param>
    /// <returns>Probability or crash</returns>
    private int ConvertThresholdToPercentage(int threshold)
    {
        switch(threshold)
        {
            case 1:
                return 1;
            case 2:
                return 4;
            case 3:
                return 16;
            default:
                throw new ArgumentOutOfRangeException("Threshold value is not mapped to percentage value.");
        }
    }
}
