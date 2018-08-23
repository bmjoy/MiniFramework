using UnityEngine;
using UnityEngine.Profiling;

namespace MiniFramework
{
    public class MemoryDetector
    {
        private readonly static string TotalAllocMemroyFormation = "Alloc Memory : {0:N}M";
        private readonly static string TotalReservedMemoryFormation = "Reserved Memory : {0:N}M";
        private readonly static string TotalUnusedReservedMemoryFormation = "Unused Reserved: {0:N}M";
        private readonly static string MonoHeapFormation = "Mono Heap : {0:N}M";
        private readonly static string MonoUsedFormation = "Mono Used : {0:N}M";
        // 字节到兆
        private float ByteToM = 0.000001f;
        public  MemoryDetector(Console console)
        {
            console.OnGUICallback += OnGUI;
        }
        void OnGUI()
        {
            GUILayout.Label(string.Format(TotalReservedMemoryFormation, Profiler.GetTotalReservedMemoryLong() * ByteToM));
            GUILayout.Label(string.Format(TotalAllocMemroyFormation, Profiler.GetTotalAllocatedMemoryLong() * ByteToM));
            GUILayout.Label(string.Format(TotalUnusedReservedMemoryFormation, Profiler.GetTotalUnusedReservedMemoryLong() * ByteToM));
            GUILayout.Label(string.Format(MonoHeapFormation, Profiler.GetMonoHeapSizeLong() * ByteToM));
            GUILayout.Label(string.Format(MonoUsedFormation, Profiler.GetMonoUsedSizeLong() * ByteToM));
        }
    }
}
