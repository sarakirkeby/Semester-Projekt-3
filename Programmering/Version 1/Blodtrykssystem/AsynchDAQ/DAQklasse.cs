using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NationalInstruments.DAQmx;
using NationalInstruments;
using System.Data;


namespace AsynchDAQ
{
    public class DAQklasse
    {
        private AnalogMultiChannelReader analogInReader;
        private Task myTask;
        private Task runningTask;
        private AsyncCallback analogCallback;

        private AnalogWaveform<double>[] data;

        private DataTable dataTable = null;
        public List<double> målingerPrivate { get; set; }

        public List<double> målinger { get; set; }


        public double SampleRate {  get; set; }
        public double MinimumVolt { get; set; } 
        public double MaximumVolt { get; set; }
        private int antalmålinger;

        public int SamplesPerChannel { get; set; }
        public string DeviceName { get; set; }

        public DAQklasse()
        {
            SampleRate = 1000;
            MinimumVolt = -1;
            MaximumVolt = 1;
            SamplesPerChannel = 200;
            DeviceName = "Dev1/ai0";
            antalmålinger = 0;
            målinger = new List<double>();
           
        }
        public DAQklasse(double samplerate, double minvolt, double maxvolt, int samplesperchannel, string devicename)
        {
            SampleRate = samplerate;
            MinimumVolt = minvolt;
            MaximumVolt = maxvolt;
            SamplesPerChannel = samplesperchannel;
            DeviceName = devicename;
            
        }

        public bool IsRunning()
        {
            if (runningTask != null)
            {
                return true;
            }
            else { return false; }
        }

        private void AnalogInCallback(IAsyncResult ar)
        {
        
            try
            {
                if (runningTask != null && runningTask == ar.AsyncState)
                {
                    // Read the available data from the channels
                    data = analogInReader.EndReadWaveform(ar);

                    // Plot your data here
                    dataToDataTable(data, ref dataTable);

                    analogInReader.BeginMemoryOptimizedReadWaveform(SamplesPerChannel,
                        analogCallback, myTask, data);
                }
            }
            catch (Exception e)
            {
                // Display Errors
                runningTask = null;
                myTask.Dispose();

            }
        }

        public void startMåling()
        {
            
            if (runningTask == null)
            {
                try
                {

                    // Create a new task
                    myTask = new Task();

                    // Create a virtual channel
                    myTask.AIChannels.CreateVoltageChannel(DeviceName, "",
                        (AITerminalConfiguration)(-1), MinimumVolt,
                        MaximumVolt, AIVoltageUnits.Volts);

                    // Configure the timing parameters
                    myTask.Timing.ConfigureSampleClock("", SampleRate,
                        SampleClockActiveEdge.Rising, SampleQuantityMode.ContinuousSamples, 1000);
                    // EVT SAMPLESPRCHANNEL!

                    // Verify the Task
                    myTask.Control(TaskAction.Verify);

                    // Prepare the table for Data


                    runningTask = myTask;
                    analogInReader = new AnalogMultiChannelReader(myTask.Stream);
                    analogCallback = new AsyncCallback(AnalogInCallback);

                    // Use SynchronizeCallbacks to specify that the object 
                    // marshals callbacks across threads appropriately.
                    analogInReader.SynchronizeCallbacks = true;
                    analogInReader.BeginReadWaveform(SamplesPerChannel,
                    analogCallback, myTask);
                }
                catch (DaqException exception)
                {
                    // Display Errors
                    runningTask = null;
                    myTask.Dispose();
                }
            }
        }
        private void dataToDataTable(AnalogWaveform<double>[] sourceArray, ref DataTable dataTable)
        {
            // Iterate over channels
            int currentLineIndex = 0;
        
            foreach (AnalogWaveform<double> waveform in sourceArray)
            {

                for (int sample = 0; sample < waveform.Samples.Count; ++sample)
                {
                    if (sample == 10)
                        break;

                    målinger.Add(waveform.Samples[sample].Value);
                    antalmålinger++;
                }
                currentLineIndex++;
                
            }
            
        }
        public int getAntalMålinger()
        {
               return målinger.Count;
        }

    }
}
