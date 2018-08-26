﻿using Caliburn.Micro;
using Mlib.Domain;
using Mlib.Domain.Infrastructure.Interfaces;
using Mlib.UI.ViewModels.Interfaces;
using System;

using System.Windows.Input;

namespace Mlib.UI.ViewModels
{
    public class AudioControlsViewModel:Screen,IViewModel
    {
        IAudioPlayer audioPlayer;
        public AudioControlsViewModel(IAudioPlayer audioPlayer)
        {
            this.audioPlayer= audioPlayer;
            StopCommand = new Command(q => audioPlayer.Stop());
            PlayCommand = new Command(q => audioPlayer.UnPause());
            PauseCommand = new Command(q => audioPlayer.Pause());
        }
        public ICommand StopCommand { get; }
        public ICommand PlayCommand { get; }
        public ICommand PauseCommand { get; }
    }
}
