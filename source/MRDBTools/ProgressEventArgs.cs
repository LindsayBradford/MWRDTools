using System;
using System.Collections.Generic;
using System.Text;

public class ProgressEventArgs : EventArgs
{
    private ProgressEventEnums.eProgress _msg;
    private int _step;
    public ProgressEventArgs(ProgressEventEnums.eProgress msg, int step)
    {
        _msg = msg;
        _step = step;
    }

    public ProgressEventEnums.eProgress Activity
    {
        get
        {
            return _msg;
        }
        set
        {
            _msg = value;
        }
    }

    public int Step
    {
        get
        {
            return _step;
        }
        set
        {
            _step = value;
        }
    }
}

public class ProgressEventEnums
{
    public enum eProgress
    {
        start = 0,
        update = 1,
        finish = 2
    }
}