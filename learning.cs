using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
/* CLASSES AND OBJECTS
namespace ConsoleApp1
{
    internal class Program
    {   string string1 = "Hello";
        int speed = 100;
        static void Main(string[] args)
        {
            Program newobj = new Program();

            Console.WriteLine(newobj.string1);
            Console.WriteLine(newobj.speed);
        }
    }
}


*/



/*PRIVATE ACCESS MODIFIER
  class School
{
   private string student = "Tanuj";

   public static void Main (string[] args)
   {
       School bag = new School();
       Console.WriteLine(bag.student);
   }
}

 */


/*INHERITENCE

class Vehicle 
{   public void Honk()
    {
        Console.WriteLine("start making noise");
    }
}

class Bike : Vehicle
{
    public static void Main(String[] args)
    {
        Bike newobj = new Bike();
        newobj.Honk();
    }
}

*/


//POLYMORPHISM

class Animal  
{
    public virtual void animalSound()
    {
        Console.WriteLine("The animal makes a sound");
    }
}

class Pig : Animal   
{
    public override void animalSound()
    {
        Console.WriteLine("The pig says: wee wee");
    }
}

class Dog : Animal  
{
    public override void animalSound()
    {
        Console.WriteLine("The dog says: bow wow");
    }
}

class Program
{
    public static void Main(string[] args)
    {
        Animal myAnimal = new Animal();  
        Animal myPig = new Pig();  
        Animal myDog = new Dog();  

        myAnimal.animalSound();
        myPig.animalSound();
        myDog.animalSound();
    }
}





