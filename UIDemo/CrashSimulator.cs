using System;
using System.Windows;

public enum CrashSimulatorType
{
    Random,
    OperationCount
}

public class CrashSimulator
{
    private readonly bool _isEnabled;
    private readonly CrashSimulatorType _type;

    /// <summary>
    /// for Random type, it represent the change of crashing, between 0 and 100, 
    /// for OperationCount, it represent the number of operation after which it crashed.
    /// </summary>
    private int _threshold;

    private int _operationCounter;

    private Random _random;

    public CrashSimulator(bool isEnabled, CrashSimulatorType type, int threshold)
	{
        _isEnabled = isEnabled;
        _type = type;
        _threshold = threshold;

        if (CrashSimulatorType.Random == _type)
            _random = new Random();
	}

    public void CrashIfItsTheTime()
    {
        if (!_isEnabled)
            return;

        if (CrashSimulatorType.Random == _type)
        {
            if (_random.Next(0, 100) >= _threshold)
                GenerateCrash();
        }
        else
        {
            // Operation count based crash
            _operationCounter++;

            if (_operationCounter >= _threshold)
                GenerateCrash();
        }
    }

    private void GenerateCrash()
    {
        // Show a modal window to inform the user that the application crashed.
        MessageBox.Show("Now is the time to crash.", "Simulated Crash", MessageBoxButton.OK, MessageBoxImage.Error);

        // Crash the application.
        throw new Exception("Simulated Crash!!!");
    }
}
