using System;
using System.Collections.Generic;
using System.Drawing;
using TowerDefense.Source.Attacks.Projectiles;
using TowerDefense.Source.Guardians.Archers;
using TowerDefense.Source.Guardians.Wizards;
using TowerDefense.Source.Monsters;

namespace TowerDefense.UI
{
    public class ImageRepository
    {
        private static readonly ImageRepository Repository = new ImageRepository();
        private readonly Dictionary<Type, string> m_typeToFileDictionary;
        private readonly Dictionary<Type, Image> m_typeToImageDictionary;

        private ImageRepository()
        {
            m_typeToFileDictionary = new Dictionary<Type, string>();
            m_typeToImageDictionary = new Dictionary<Type, Image>();
            InitFileDictionary();
        }

        public static ImageRepository GetInstance()
        {
            return Repository;
        }

        public Image GetImage(Type type)
        {
            if (m_typeToImageDictionary.ContainsKey(type))
            {
                return m_typeToImageDictionary[type];
            }
            if (m_typeToFileDictionary.ContainsKey(type))
            {
                var image = Image.FromFile(m_typeToFileDictionary[type]);
                m_typeToImageDictionary.Add(type, image);
                return image;
            }
            throw new Exception("Could not find Image for supplied type.");
        }

        private void InitFileDictionary()
        {
            m_typeToFileDictionary.Add(typeof(Bubble), @"..\..\Images\bubble.png");
            m_typeToFileDictionary.Add(typeof(Skull), @"..\..\Images\skull.png");
            m_typeToFileDictionary.Add(typeof(Arrow), @"..\..\Images\proj.png");
            m_typeToFileDictionary.Add(typeof(FireWizard), @"..\..\Images\enem.png");
            m_typeToFileDictionary.Add(typeof(IceWizard), @"..\..\Images\enem.png");
            m_typeToFileDictionary.Add(typeof(DarkArcher), @"..\..\Images\enem.png");
            m_typeToFileDictionary.Add(typeof(LightArcher), @"..\..\Images\enem.png");
            m_typeToFileDictionary.Add(typeof(MageBall), @"..\..\Images\enem.png");
        }
    }
}
