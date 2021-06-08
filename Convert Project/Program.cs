using System;
using System.Collections.Generic;
using Convert_Project.Managers;

namespace Convert_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            GameManager manager = new GameManager();
            
            manager.GameLoop();
        }
    }
}
