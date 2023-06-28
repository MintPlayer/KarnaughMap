namespace MintPlayer.GrayConverter.Abstractions;

public interface IGrayConverter
{
    int Decimal2Gray(int n);
    int Gray2Decimal(int n);
}