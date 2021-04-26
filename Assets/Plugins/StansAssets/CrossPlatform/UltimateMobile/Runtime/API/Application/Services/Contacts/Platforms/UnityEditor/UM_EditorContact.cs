using System;
using UnityEngine;

namespace SA.CrossPlatform.App
{
    [Serializable]
    class UM_EditorContact : UM_AbstractContact, UM_iContact
    {
        public UM_EditorContact(string name, string phone, string email)
        {
            Name = name;
            Phone = phone;
            JobTitle = email;
        }

        public new string Name
        {
            get => base.Name;
            set => base.Name = value;
        }

        public new string Phone
        {
            get => base.Phone;
            set => base. Phone = value;
        }

        public new string Email
        {
            get => base. Email;
            set => base. Email = value;
        }

        public UM_EditorContact Clone()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<UM_EditorContact>(json);
        }
    }
}
