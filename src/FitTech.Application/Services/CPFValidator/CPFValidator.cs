namespace FitTech.Application.Services.CPFValidator
{
    public class CPFValidator
    {
        public bool ValidateCPF(string cpf)
        {
            cpf = new string(cpf.Where(char.IsDigit).ToArray());
            if (cpf.Length != 11)
            {
                return false;
            }

            // Calculate the first verification digit
            int sum = 0;

            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(cpf[i].ToString()) * (10 - i);
            }

            int remainder = (sum * 10) % 11;

            if (remainder == 10 || remainder == 11)
            {
                remainder = 0;
            }

            if (remainder != int.Parse(cpf[9].ToString()))
            {
                return false;
            }

            // Calculate the second verification digit
            sum = 0;

            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(cpf[i].ToString()) * (11 - i);
            }

            remainder = (sum * 10) % 11;

            if (remainder == 10 || remainder == 11)
            {
                remainder = 0;
            }

            if (remainder != int.Parse(cpf[10].ToString()))
            {
                return false;
            }

            // If all checks pass, the CPF is valid
            return true;
        }
    }
}
