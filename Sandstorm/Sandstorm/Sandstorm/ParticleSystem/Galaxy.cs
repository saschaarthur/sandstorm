﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sandstorm.ParticleSystem.draw;
using Sandstorm.ParticleSystem.physic;
using Sandstorm.ParticleSystem.structs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Sandstorm.Terrain;

namespace Sandstorm.ParticleSystem
{
    public class Galaxy
    {
        private DrawEngine _drawEngine = null; //Draw-Engine
        private PhysicEngine _physicEngine = null; //PhysicEngine
        public SharedList _sharedList = new SharedList(); //SharedList of Particles

        private List<Emiter> _emiters = new List<Emiter>();

        private Camera _camera;
        private GraphicsDevice _graphicsDevice;
        private ContentManager _contentManager;
        private HeightMap _heightMap;

        public Galaxy(GraphicsDevice pGraphicsDevice, ContentManager pContentManager, Camera pCamera, HeightMap heightMap)
        {
            _emiters.Add(new Emiter(new Vector3(0f, 100f, 0f), new Vector3(1.0f, 1f, 1.0f), _sharedList));
            _emiters.Add(new Emiter(new Vector3(100f, 100f, 0f), new Vector3(-1.0f, 1f, 1.0f), _sharedList));
            _emiters.Add(new Emiter(new Vector3(0f, 100f, 100f), new Vector3(1.0f, 1f, 11.0f), _sharedList));
            _emiters.Add(new Emiter(new Vector3(100f, 100f, 100f), new Vector3(-1.0f, 1f, -1.0f), _sharedList));

            _camera = pCamera;
            _graphicsDevice = pGraphicsDevice;
            _contentManager = pContentManager;

            _heightMap = heightMap;

            _drawEngine = new DrawEngine(pGraphicsDevice, pContentManager, pCamera, _sharedList);
            _physicEngine = new PhysicEngine(_sharedList, _heightMap);
        }


        public void Update(GameTime pGameTime)
        {
            foreach (Emiter e in _emiters)
            {
                e.emit();
            }
            _drawEngine.Update(pGameTime);
            _physicEngine.Update(pGameTime);
        }

        public void Draw()
        {
            _physicEngine.Draw();
            _drawEngine.Draw(_drawEngine.getFPS(), _physicEngine.getFPS());
        }
    }
}
