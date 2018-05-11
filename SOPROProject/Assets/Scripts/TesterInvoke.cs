using UnityEngine;
public class TesterInvoke : MonoBehaviour
{

    public float ValueFloat;
    public int ValueInt;
    public string ValueString;
    public byte ValueByte;

    public SOEventFloatIntStringByte Boh;

    public void RaiseEvent()
    {
        Boh.Raise(ValueFloat, ValueInt, ValueString, ValueByte);
    }
    public void InvokeListener(float f, int i, string s, byte b)
    {
        Debug.LogFormat("Float = {0} , int = {1} , string = {2} , byte = {3}", f, i, s, b);
    }
}