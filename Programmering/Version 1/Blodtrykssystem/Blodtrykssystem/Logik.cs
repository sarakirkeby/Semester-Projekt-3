﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Threading;
using NS_Data;
using Blodtrykssystem;

namespace NS_Logik
{
    class Logik : ISubject
    {
        Data datalag;
        Thread t2;
        public Logik()
        {
            datalag = new Data();
            grafcount = 0;
            antalpåxakse = 400;
            optæller = 0;
            initListe();
            grafdata = new List<double>();

        }
        public void startMåling()
        {
            t2 = new Thread(getGrafData);
            t2.Start();
        }
        private int antalpåxakse;
        public int getAntalMålinger()
        {
            return datalag.getAntalMålinger();
        }
        private DTO_Blodtryksmåling målinger;
        //public DTO_Blodtryksmåling getMålinger()
        //{

        //   return datalag.getMålinger();
            
        //}
        private int grafcount;
        public List<double> grafdata;

        private void initListe()
        {
            xværdier = new List<double>();
            for(int i = 0; i < antalpåxakse; i++)
            {
                xværdier.Add(0);
               
            }
            
            
        }
        private List<double> xværdier;
        private int optæller;

        public void getGrafData()
        {

            while (IsRunning())
            {
                målinger = datalag.HentData();
                List<double> liste = new List<double>();
                liste.Clear();
                liste = målinger.getMålinger1();

                int antal = liste.Count;

                if (antal > optæller && grafcount < antalpåxakse)


                {
                    xværdier[grafcount] = liste[optæller];
                    grafcount++;
                    optæller++;
                    grafdata = xværdier;

                    Notify();
                }
                if (grafcount == antalpåxakse)
                {
                    grafcount = 0;
                }
 
                

            }
            //if (antal > 100*(grafcount+1))
            //{
            //    for (int i = 1 * grafcount; i < 100*grafcount+99; i++)
            //    {
            //        xværdier.Add(liste[i]);
            //    }

                

            //}
            
            
        }

        public bool IsRunning()
        {
            return datalag.IsRunning();
        }

        private List<Form1> observers = new List<Form1>();

        public void Subscribe(Form1 observer)
        {
            observers.Add(observer);
        }

        public void Unsubscribe(Form1 observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
                observers[0].Opdater();
        }
    




}


}
