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

using PixelVision8.Engine.Chips;
using PixelVision8.Engine.Services;
using System.Collections.Generic;

namespace PixelVision8.Engine
{

    public interface IMetaData
    {
        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        string GetMetadata(string key, string defaultValue = "");

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetMetadata(string key, string value);

        /// <summary>
        /// </summary>
        /// <param name="target"></param>
        /// <param name="ignoreKeys"></param>
        void ReadAllMetadata(Dictionary<string, string> target);
    }

    /// <summary>
    ///     This is the default engine class for Pixel Vision 8. It manages the
    ///     state of all chips, the game itself and helps with communication between
    ///     the two.
    /// </summary>
    public class PixelVisionEngine : PixelVisionEngineLite, IServiceLocator, IMetaData
    {
        // protected Dictionary<string, AbstractChip> Chips = new Dictionary<string, AbstractChip>();
        // protected string[] DefaultChips;
        // protected List<IDraw> DrawChips = new List<IDraw>();
        protected IServiceLocator ServiceLocator;
        // protected List<IUpdate> UpdateChips = new List<IUpdate>();

        public Dictionary<string, string> MetaData { get; } = new Dictionary<string, string>
        {
            {"name", "untitled"}
        };

        public PixelVisionEngine(IServiceLocator serviceLocator, string[] chips = null, string name = "Engine")
        {

            ServiceLocator = serviceLocator;

            //if (chips != null) DefaultChips = chips;

            Name = name;

            //apiBridge = new APIBridge(this);
            if (chips != null)
            {
                foreach (var chip in chips) GetChip(chip);
            }

            // Init();
        }

        /// <summary>
        ///     Access to the MusicChip.
        /// </summary>
        /// <tocexclude />
        public MusicChip MusicChip { get; set; }

        public void AddService(string id, IService service)
        {
            ServiceLocator.AddService(id, service);
        }

        public IService GetService(string id)
        {
            return ServiceLocator.GetService(id);
        }

        public void RemoveService(string id)
        {
            ServiceLocator.RemoveService(id);
        }


        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public string GetMetadata(string key, string defaultValue = "")
        {
            if (!MetaData.ContainsKey(key)) MetaData.Add(key, defaultValue);

            return MetaData[key];
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetMetadata(string key, string value)
        {
            if (!MetaData.ContainsKey(key))
            {
                MetaData.Add(key, value);
            }
            else
            {
                if (value == "")
                    MetaData.Remove(key);
                else
                    MetaData[key] = value;

            }
        }

        /// <summary>
        /// </summary>
        /// <param name="target"></param>
        /// <param name="ignoreKeys"></param>
        public void ReadAllMetadata(Dictionary<string, string> target)
        {
            target.Clear();

            foreach (var data in MetaData) target.Add(data.Key, data.Value);
        }

    }
}