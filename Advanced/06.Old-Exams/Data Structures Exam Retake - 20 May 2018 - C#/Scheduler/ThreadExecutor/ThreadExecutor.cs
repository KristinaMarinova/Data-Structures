using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

/// <summary>
/// The ThreadExecutor is the concrete implementation of the IScheduler.
/// You can send any class to the judge system as long as it implements
/// the IScheduler interface. The Tests do not contain any <e>Reflection</e>!
/// </summary>
public class ThreadExecutor : IScheduler
{

  
    public ThreadExecutor()
    {

    }

    public int Count
    {
        return 0;
    }


    public void ChangePriority(int id, Priority newPriority)
    {

       
    }

    public bool Contains(Task task)
    {
        return false;
    }

    public int Cycle(int cycles)
    {

       return 0

    }

    public void Execute(Task task)
    {
		
    }

    public IEnumerable<Task> GetByConsumptionRange(int lo, int hi, bool inclusive)
    {
       return null
    }

    public Task GetById(int id)
    {
        return null;
    }

    public Task GetByIndex(int index)
    {
        return null;
    }

    public IEnumerable<Task> GetByPriority(Priority type)
    {
        return null;
    }

    public IEnumerable<Task> GetByPriorityAndMinimumConsumption(Priority priority, int lo)
    {
        return null;
    }


    public IEnumerator<Task> GetEnumerator()
    {
        return null;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
