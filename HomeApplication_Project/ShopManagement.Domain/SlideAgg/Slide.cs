﻿using _0_Framework.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopManagement.Domain.SlideAgg
{
    public class Slide : EntityBase
    {
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Heading { get; private set; }
        public string Title { get; private set; }
        public string Text { get; private set; }
        public string ButtonText { get; private set; }
        public bool IsRemoved { get; private set; }

        public Slide(string picture, string pictureAlt, string pictureTitle, string heading,
            string title, string text, string buttonText)
        {
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Heading = heading;
            Title = title;
            Text = text;
            ButtonText = buttonText;
            IsRemoved = false;
        }

        public void Edit(string picture, string pictureAlt, string pictureTitle, string heading,
            string title, string text, string btnText)
        {
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Heading = heading;
            Title = title;
            Text = text;
            ButtonText = btnText;
        }

        public void Remove()
        {
            IsRemoved = true;
        }

        public void Restore()
        {
            IsRemoved = false;
        }
    
    }
}