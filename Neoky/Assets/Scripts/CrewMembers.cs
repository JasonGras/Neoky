using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class CrewMembers : MonoBehaviour
    {
        public int MemberPosition;
        public string MemberName;
        public float MemberHP;

        public void Initialize(int _memberPosition, string _memberName, float _memberHP)
        {
            MemberPosition = _memberPosition;
            MemberName = _memberName;
            MemberHP = _memberHP;
        }

    }
}
