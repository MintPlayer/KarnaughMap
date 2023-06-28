using Microsoft.Extensions.DependencyInjection;
using MintPlayer.PetricksMethod.Abstractions;
using MintPlayer.PetricksMethod.Extensions;
using MintPlayer.QuineMcCluskey.Abstractions;
using MintPlayer.QuineMcCluskey.Extensions;
using MintPlayer.GrayConverter.Extensions;

namespace MintPlayer.KarnaughMap.Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var services = new ServiceCollection()
                .AddQuineMcCluskey()
                .AddPetricksMethod()
                .AddGrayConverter()
                .BuildServiceProvider();

            karnaughMap1.QuineMcCluskey = services.GetRequiredService<IQuineMcCluskey>();
            karnaughMap1.PetricksMethod = services.GetRequiredService<IPetricksMethod>();
        }
    }
}