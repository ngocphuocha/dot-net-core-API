using TodoApp.Services.IServices;

namespace TodoApp.Services
{
    public class Caculator : ICaculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}
