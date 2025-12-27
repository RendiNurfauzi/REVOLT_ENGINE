using System.Diagnostics;

namespace Revolt.Core;

public class CoreEngine
{
    private readonly List<IModuleSystem> _systems = new();
    public bool IsRunning { get; private set; }
    private Stopwatch _stopwatch = new();

    public void RegisterSystem(IModuleSystem system)
    {
        _systems.Add(system);
        _systems.Sort((a, b) => a.Priority.CompareTo(b.Priority));
        
        system.OnAwake();
        Console.WriteLine($"[Core] System Registered: {system.Name}");
    }

    public void Run()
    {
        if (IsRunning) return;
        IsRunning = true;

        foreach (var system in _systems) system.OnStart();

        _stopwatch.Start();
        double lastTime = 0;

        while (IsRunning)
        {
            double currentTime = _stopwatch.Elapsed.TotalSeconds;
            double deltaTime = currentTime - lastTime;
            lastTime = currentTime;
            foreach (var system in _systems)
            {
                system.OnUpdate(deltaTime);
            }
            Thread.Sleep(1); 
        }

        foreach (var system in _systems) system.OnShutdown();
    }

    public void Stop() => IsRunning = false;
}