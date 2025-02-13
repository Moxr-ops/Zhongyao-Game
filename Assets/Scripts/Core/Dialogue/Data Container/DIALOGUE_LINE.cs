using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DIALOGUE
{
    public class DIALOGUE_LINE
    {
        public DL_SPEAKER_DATA speakerData;
        public string dialogueData;
        public DL_COMMAND_DATA commandData;

        public bool hasSpeaker => speakerData != null; // speakerData != string.Empty;
        public bool hasDialogue => dialogueData != string.Empty;
        public bool hasCommand => commandData != null;

        public DIALOGUE_LINE(string speaker, string dialogue, string commands)
        {
            this.speakerData = (string.IsNullOrWhiteSpace(speaker) ? null : new DL_SPEAKER_DATA(speaker));
            this.dialogueData = (string.IsNullOrEmpty(dialogue) ? null : dialogue);
            this.commandData = (string.IsNullOrWhiteSpace(commands) ? null : new DL_COMMAND_DATA(commands));
        }
    }
}