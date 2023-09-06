using System;
using System.Runtime.InteropServices;

//ref link:https://www.youtube.com/watch?v=fYIv22BUCK8&list=PLRwVmtr-pp07XP8UBiUJ0cyORVCmCgkdA&index=40
// sizeof operator -- inherited from C++
// sizeof operator -- compile type operator
// sizeof value/struct type layout opertion -- sequential, auto, Explicit

//class MeUglyClass   // managed type - from C++, runtime operations
//[StructLayout(LayoutKind.Sequential)]   // 12
//[StructLayout(LayoutKind.Auto)]   // 8  -- compiler auto packing 
//struct MeUglyClass
//{
//    public char meChar1;
//    public int meInt;
//    public char meChar2;
//}
[StructLayout(LayoutKind.Explicit)]   // 16 -- can manipulate the alignment and packing
struct MeUglyClass
{
    [FieldOffset(0)]        // 99 00 00 00  - meChar1
    public char meChar1;    // 00 00 99 00  - meChar2
    [FieldOffset(12)]       // 00 00 00 00
    public int meInt;       // 99 00 00 00  - meInt
    [FieldOffset(6)]        
    public char meChar2;    
}


//struct MeUglyClass      // for unsafe sizeof operations -- must match alignment and packing
//{   // output: 12 -- wasted 4 bytes cause of sequencing alignment of struct
//    public char meChar1;        // 2bytes(used) + 2bytes(wasted) -- 99 00
//    public int meInt;           // 4 bytes(used)                 -- 99 99
//    public char meChar2;        // 2bytes(used) + 2bytes(wasted) -- 99 00
//}

//{   // output 8 -- sequencing alignment + packing(meChar1 + meChar2)
//    public char meChar1;      //  99 99
//    public char meChar2;      //     
//    public int meInt;         //  99 99
//}

class MainClass
{
    static void Main()
    {
        MeUglyClass mug =
            new MeUglyClass();
        mug.meChar1 = 'j';
        mug.meInt = 25;
        mug.meChar2 = 'k';
        //Console.WriteLine(sizeof(MeUglyClass)); // error c# cant compile cause of alignment and packing issues
        //Console.WriteLine(sizeof(int)); // c# can compile

        unsafe        // type object pointer knowledge
        { 
        Console.WriteLine(sizeof(MeUglyClass));
        }
    }
}