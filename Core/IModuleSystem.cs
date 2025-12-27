namespace Revolt.Core;

public interface IModuleSystem
{
    string Name { get; }
    int Priority { get; } // Graphics: 0, Engine: 100, Game: 200

    void OnAwake();   // Inisialisasi awal (Register)
    void OnStart();   // Sebelum loop dimulai
    void OnUpdate(double dt);
    void OnShutdown();
}