using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;
using System.Diagnostics;

namespace EternalEngine
{
    [Serializable]
    public class Animation
    {
        public Animation()
        {
            Keyframes = new SortedList<int, Keyframe>();
            NumberofFrames = 100;
            CurrentFrame = 0;
        }

        public Animation(FileStream file)
        {
            CurrentFrame = 0;
            SoapFormatter sf = new SoapFormatter();
            Animation deserial = (Animation)sf.Deserialize(file);
            Keyframes = deserial.Keyframes;
            Displacement = deserial.Displacement;
            ModelName = deserial.ModelName;
            NumberofFrames = deserial.NumberofFrames;
            file.Close();
        }

        public void GoTo(List<Vertex> vertices, int frame)
        {
            CurrentFrame = frame;
            if (PreviousFrame != -1 && NextFrame != -1)
            {
                float percent = 0;
                try {
                    percent = (((float)CurrentFrame - (float)PreviousFrame) / ((float)NextFrame - (float)PreviousFrame));
                    if (CurrentFrame == 0) { percent = 0; }
                    if (CurrentFrame == NumberofFrames) { percent = 1; }
                }
                catch (DivideByZeroException)
                {
                    Debug.WriteLine("Error: Division by 0 @ calculation of percent");
                }
                Debug.WriteLine("calculating! " + CurrentFrame.ToString() + " " + PreviousFrame.ToString() + " " + NextFrame + " percent:" + percent.ToString());
                for (int i = 0; i < Keyframes[PreviousFrame].Vertices.Count; i++)
                {
                    vertices[i].LocationX = Keyframes[PreviousFrame].Vertices[i].LocationX + (percent * (Keyframes[NextFrame].Vertices[i].Location.X - Keyframes[PreviousFrame].Vertices[i].Location.X));
                    vertices[i].LocationY = Keyframes[PreviousFrame].Vertices[i].LocationY + (percent * (Keyframes[NextFrame].Vertices[i].Location.Y - Keyframes[PreviousFrame].Vertices[i].Location.Y));
                }
            }
            else
                Debug.WriteLine("Error: Previous or Next Frames == -1 @ GoTo()");
        }

        public int CurrentFrame { get; set; }

        public SortedList<int, Keyframe> Keyframes { get; set; }

        /// <summary>
        /// Displacement per frame through 2D space
        /// </summary>
        public PointF Displacement { get; set; }

        public int PreviousFrame
        {
            get
            {
                int final = CurrentFrame;
                bool success = false;
                for (int i = 0; i <= CurrentFrame; i++)
                {
                    if (Keyframes.ContainsKey(i))
                    {
                        final = i;
                        success = true;
                    }
                }
                if (success)
                {
                    return final;
                }
                else { return -1; }
            }
        }

        public int NextFrame 
        {
            get
            {
                int final = CurrentFrame;
                bool success = false;
                for (int i = NumberofFrames; i > CurrentFrame; i--)
                {
                    if (Keyframes.ContainsKey(i))
                    {
                        final = i;
                        success = true;
                    }
                }
                if (success)
                {
                    return final;
                }
                else { return -1; }
            } 
        }

        public int NumberofFrames { get; set; }

        /// <summary>
        /// The model the animation applies to
        /// </summary>
        public string ModelName { get; set; }
    }
}
