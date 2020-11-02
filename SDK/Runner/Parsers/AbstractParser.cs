﻿//   
// Copyright (c) Jesse Freeman, Pixel Vision 8. All rights reserved.  
//  
// Licensed under the Microsoft Public License (MS-PL) except for a few
// portions of the code. See LICENSE file in the project root for full 
// license information. Third-party libraries used by Pixel Vision 8 are 
// under their own licenses. Please refer to those libraries for details 
// on the license they use.
// 
// Contributors
// --------------------------------------------------------
// This is the official list of Pixel Vision 8 contributors:
//  
// Jesse Freeman - @JesseFreeman
// Christina-Antoinette Neofotistou @CastPixel
// Christer Kaitila - @McFunkypants
// Pedro Medeiros - @saint11
// Shawn Rakowski - @shwany
//

using 
/* Unmerged change from project 'PixelVision8.CoreDesktop'
Before:
using System.Collections.Generic;
using PixelVision8.Runner.Services;
After:
using System.Collections.Services;
using PixelVision8.Runner.Utils;
using System;
*/
PixelVision8.Runner.Utils;
using System;
using System.Collections.Generic;

namespace PixelVision8.Runner.Parsers
{
    public abstract class AbstractParser : IAbstractParser
    {
        protected List<Action> _steps = new List<Action>();

        public IFileLoadHelper FileLoadHelper;

        public int CurrentStep { get; protected set; }

        public string SourcePath;

        public virtual byte[] bytes { get; set; }

        public int totalSteps => _steps.Count;

        public bool completed => CurrentStep >= totalSteps;

        public virtual void CalculateSteps()
        {
            CurrentStep = 0;

            // First step will always be to get the data needed to parse
            if (!string.IsNullOrEmpty(SourcePath))
                _steps.Add(LoadSourceData);
        }

        public virtual void LoadSourceData()
        {
            if (FileLoadHelper != null)
            {
                bytes = FileLoadHelper.ReadAllBytes(SourcePath);
            }

            StepCompleted();
        }

        public virtual void NextStep()
        {
            if (completed) return;

            _steps[CurrentStep]();
        }

        public virtual void StepCompleted()
        {
            CurrentStep++;
        }

        public virtual void Dispose()
        {
            bytes = null;
            FileLoadHelper = null;
            _steps.Clear();
        }
    }
}