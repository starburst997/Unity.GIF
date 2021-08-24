using System;
using UnityEngine;

namespace jd.boivin.unity.gif
{
    public class GIFImage //: ICloneable
    {
        public int       Width;
        public int       Height;
        public int       Delay; // milliseconds
        public Color32[] RawImage;

        public GIFImage()
        {
        }

        /*public GIFImage( GIFImage img )
        {
            Width    = img.Width;
            Height   = img.Height;
            Delay    = img.Delay;
            RawImage = img.RawImage != null ? (Color32[]) img.RawImage.Clone() : null;
        }

        public object Clone()
        {
            return new GIFImage( this );
        }*/

        public Texture2D CreateTexture()
        {
            var tex = new Texture2D( Width, Height, TextureFormat.ARGB32, false )
            {
                filterMode = FilterMode.Point,
                wrapMode   = TextureWrapMode.Clamp
            };

            tex.SetPixels32( RawImage );
            tex.Apply();

            return tex;
        }

        public void Dispose()
        {
            // TODO: Return `RawImage` to pool
        }
    }
}