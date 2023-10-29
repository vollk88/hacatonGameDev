using System;
using System.Collections.Generic;
using System.Linq;
using BaseClasses;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Audio
{
    [Serializable, RequireComponent(typeof(StudioEventEmitter))]
    public class SoundManager : CustomBehaviour
    {
        [Tooltip("Путь к инстансам звуков")]
        [SerializeField] protected string[] _soundsInstancePath;
        [Tooltip("Путь к инстансам зацикленных звуков")]
        [SerializeField] protected string[] _soundsLoopInstancePath;
        [Tooltip("Путь к инстансам звуков шагов"), SerializeField] 
        protected string _soundFootstepInstancePath;
    
        /// <summary>Звуки шагов.</summary>
        private EventInstance _soundFootstepInstance;
        private EventReference _footstepReference;
        /// <summary>Зациклинные звуки.</summary>
        private List<EventInstance> _soundsLoopInstance;
        private List<EventReference> _loopReferences;
        /// <summary>OneShot звуки.</summary>
        private List<EventInstance> _soundInstance;
        private List<EventReference> _eventReferences;

        // private PLAYBACK_STATE pS;
        [GetOnObject]
        private StudioEventEmitter _studioEvent;

        protected override void Awake()
        {
            base.Awake();
            _studioEvent.StopEvent = EmitterGameEvent.ObjectDisable;
            if (_soundsLoopInstancePath.Length > 0)
                CreateInstanceAndReference(_soundsLoopInstancePath, ref _soundsLoopInstance, ref _loopReferences);

            if (_soundsInstancePath.Length > 0)
                CreateInstanceAndReference(_soundsInstancePath, ref _soundInstance, ref _eventReferences);
        
            if (_soundFootstepInstancePath.Length > 0)
            {
                _footstepReference.Guid = RuntimeManager.PathToGUID(_soundFootstepInstancePath);
                _footstepReference.Path = _soundFootstepInstancePath;
                _soundFootstepInstance = RuntimeManager.CreateInstance(_soundFootstepInstancePath);
            }
        }

        private void CreateInstanceAndReference(IReadOnlyCollection<string> soundInstancePath,
            ref List<EventInstance> soundInstance, 
            ref List<EventReference> eventReferences)
        {
            soundInstance = new List<EventInstance>(soundInstancePath.Count);
            eventReferences = new List<EventReference>(soundInstancePath.Count);
            foreach (string path in soundInstancePath)
            {
                RESULT result = RuntimeManager.StudioSystem.lookupID(path, out GUID _);
            
                if(path == "-" || result == RESULT.ERR_EVENT_NOTFOUND)
                {
                    eventReferences.Add(default);
                    soundInstance.Add(default);
                    continue;
                }

                EventReference reference;
                reference.Guid = RuntimeManager.PathToGUID(path);
                reference.Path = path;
                eventReferences.Add(reference);
                soundInstance.Add(RuntimeManager.CreateInstance(path));
            }
        }

        public void PlayStartSounds(int i)
        {
            if(_soundsLoopInstance == null || _soundsLoopInstance.Count < i) return;
            SetNewSound(_loopReferences[i], _soundsLoopInstance[i]);
            _studioEvent.Play();
        }


        public void StopSound() => _studioEvent.Stop();

        // ReSharper disable Unity.PerformanceAnalysis
        public void PlaySound(int i)
        {
            if (_soundsInstancePath == null || _soundsInstancePath.Length <= i 
                                            || _eventReferences[i].Path == null) return;
#if UNITY_WEBGL
            if (PlaybackState(_soundInstance[i]) == PLAYBACK_STATE.PLAYING)
                _soundInstance[i].stop(STOP_MODE.ALLOWFADEOUT);
#endif
        
            SetNewSound(_eventReferences[i], _soundInstance[i]);
            _studioEvent.Play();
        }

        private void SetNewSound(EventReference eventReference, EventInstance soundInstance)
        {
            _studioEvent.EventReference = eventReference;
            _studioEvent.SetInstance(soundInstance);
        }

        public void FootstepSound(int i)
        {
            if(_soundFootstepInstancePath.Length == 0) return;
        
            if(_studioEvent.EventReference.Path != _footstepReference.Path)
                SetNewSound(_footstepReference, _soundFootstepInstance);
        
            if(i != -1)
                _studioEvent.EventInstance.setParameterByName("P_Surface", i);
            _studioEvent.Play();
        }

        /// <summary>
        /// Остановка всех звуковых ивентов при переключении вкладки или сворачивании окна
        /// </summary>
        private void OnApplicationFocus(bool hasFocus)
        {
            RuntimeManager.MuteAllEvents(!hasFocus);
        }

        private void OnDestroy()
        {
            if(_soundInstance is not null)
            {
                foreach (EventInstance sound in _soundInstance)
                {
                    sound.stop(STOP_MODE.IMMEDIATE);
                    sound.release();
                }
            }
        
            _soundFootstepInstance.stop(STOP_MODE.IMMEDIATE);
            _soundFootstepInstance.release();
        
            if(_soundsLoopInstance is null) return;
        
            foreach (EventInstance sound in _soundsLoopInstance)
            {
                sound.stop(STOP_MODE.IMMEDIATE);
                sound.release();
            }
        }
    }
}
