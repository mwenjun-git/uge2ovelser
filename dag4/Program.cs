//Bit operationer fra papir øvelser
byte x = 0b0110_0101; //'e'
void VisN(int n)
{
    Console.WriteLine($"Numerisk er n{n},som er tegnet{(char)n},binært {Convert.ToString(n,2)}");
}
VisN(x);
int a = x << 1;
VisN(a);

VisN(x>>3);
VisN(x & 0b1111);
VisN((x >> 5)&1);
VisN((x | 0b0000_0110));

//Modulus
for(int i =0; i<25;i++)
{
     if(i%5==0)
    {
        Console.Write("");
    }
}