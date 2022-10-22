using System.Collections.Concurrent;
using UnityEngine;

namespace jd.boivin.unity.gif
{
    // Allow to recycle GIF instance
    public static class GIFManager
    {
        private static ConcurrentQueue<GIFDecoder> Decoders = new ConcurrentQueue<GIFDecoder>();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void ResetStatic()
        {
            Decoders = new ConcurrentQueue<GIFDecoder>();
        }
        
        public static void Initialize()
        {
            for (var i = 0; i < 4; i++)
                Decoders.Enqueue(new GIFDecoder());
        }
        
        public static void Clear()
        {
            while (Decoders.TryDequeue(out var decoder))
            {
                decoder.Dispose();
            }
        }
        
        public static void Return(GIFDecoder decoder)
        {
            if (decoder == null) return;
            decoder.Clear();
            
            Decoders.Enqueue(decoder);
        }

        public static GIFDecoder Get()
        {   
            if (Decoders.TryDequeue(out var decoder))
            {
                decoder.Reset();
                return decoder;
            }
            
            var newDecoder = new GIFDecoder();
            newDecoder.Reset();

            return newDecoder;
        }
    }
}