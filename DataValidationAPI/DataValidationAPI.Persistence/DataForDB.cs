using DataValidationAPI.Domain.Entities;

namespace DataValidationAPI.Persistence
{
    /// <summary>
    /// Данные по умолчанию для базы данных
    /// </summary>
    public static class DataForDB
    {
        /// <summary>
        /// Роли
        /// </summary>
        public static List<Role> Roles { get; } = new List<Role>()
        {
            new Role { Id = new Guid("37491307-9159-465b-a902-855c7c315341"), Name = Domain.Constants.Roles.User },
            new Role { Id = new Guid("61631b6b-22d3-4a61-83d7-288ed59be881"), Name = Domain.Constants.Roles.Manager },
            new Role { Id = new Guid("7f7d960c-dfc3-4379-b1ec-6112e827862f"), Name = Domain.Constants.Roles.Admin },
        };

        /// <summary>
        /// Пользователи
        /// </summary>
        public static List<User> Users { get; } = new List<User>()
        {
            new User() { Id = new Guid("35434031-0853-472c-8c87-3b7831e0fd17"), Email = "1", RoleId = Roles[0].Id, PasswordHash = "yE3UT3m6W4KlkNLJdTAM4UQYIZuS7QIU/6kWcAjU/mc=", PasswordSalt = new byte[] { 118 ,75 ,18 ,36 ,222 ,48 ,190 ,38 ,185 ,49 ,119 ,151 ,113 ,34 ,164 ,228 }, RefreshToken = null, TokenExpirationDate = null, IsActive = false },
            new User() { Id = new Guid("f725550b-b2b0-4509-85bd-556535471756"), Email = "2", RoleId = Roles[0].Id, PasswordHash = "KjK/J9gOLsJ5Qi8MQlpn7+G5YcU1ZnQtuZI1X+TTzy0=", PasswordSalt = new byte[] { 137 ,110 ,127 ,108 ,129 ,137 ,155 ,42 ,178 ,110 ,230 ,77 ,29 ,222 ,131 ,149 }, RefreshToken = null, TokenExpirationDate = null, IsActive = true },
            new User() { Id = new Guid("2efa388c-584b-4e8d-9c5d-29fa600cfac9"), Email = "3", RoleId = Roles[1].Id, PasswordHash = "mVJOzj048UABroLpqaiOCXC7ov4rY/bHqr8zYznk+2I=", PasswordSalt = new byte[] { 34 ,54 ,150 ,248 ,77 ,23 ,108 ,37 ,149 ,29 ,207 ,94 ,119 ,12 ,110 ,183 }, RefreshToken = null, TokenExpirationDate = null, IsActive = true },
            new User() { Id = new Guid("ead9e0c0-395b-4489-a82b-416562905957"), Email = "4", RoleId = Roles[1].Id, PasswordHash = "Kfqg0txtZSqNkmeQbOosmGadf/IIaB2z3WaeMr3C1o0=", PasswordSalt = new byte[] { 205 ,233 ,1 ,141 ,87 ,98 ,77 ,54 ,135 ,152 ,169 ,87 ,148 ,116 ,188 ,201 }, RefreshToken = null, TokenExpirationDate = null, IsActive = true },
            new User() { Id = new Guid("adca4721-3a4c-44ca-80a6-de74abc7450e"), Email = "5", RoleId = Roles[2].Id, PasswordHash = "4PDjB8dRo5n/m9N7vnYR8tM/PdyB0M7wV+dHNRAD3YQ=", PasswordSalt = new byte[] { 51 ,48 ,107 ,15 ,165 ,157 ,219 ,223 ,108 ,49 ,81 ,49 ,152 ,219 ,5 ,159 }, RefreshToken = null, TokenExpirationDate = null, IsActive = true },
            new User() { Id = new Guid("f5080b8b-9b17-497b-8fe3-9470978b0ab1"), Email = "6", RoleId = Roles[2].Id, PasswordHash = "Pmuehs/dEiTauUtjQXqPWSjr4XombKHPuqgZBW1JYhM=", PasswordSalt = new byte[] { 128 ,183 ,146 ,79 ,41 ,2 ,134 ,246 ,132 ,144 ,191 ,89 ,214 ,199 ,85 ,70 }, RefreshToken = null, TokenExpirationDate = null, IsActive = true },
        };

        /// <summary>
        /// Данные
        /// </summary>
        public static List<Data> Datas { get; } = new List<Data>()
        {
            new Data() { Id = new Guid("c43cc9e3-530c-446b-9acf-644d08941f79"), Date = new DateTime(23, 1, 1), Information = "Информация 1", PersonProvidedId = Users[0].Id },
            new Data() { Id = new Guid("70baeb6b-e3f3-4c3c-a6d0-22c1aca1df4f"), Date = new DateTime(23, 2, 2), Information = "Информация 2", PersonProvidedId = Users[0].Id },
            new Data() { Id = new Guid("9e0bac5b-2c58-43a7-b548-99a4a94728fc"), Date = new DateTime(23, 3, 3), Information = "Информация 3", PersonProvidedId = Users[0].Id },
            new Data() { Id = new Guid("c0267569-6cb2-4009-8425-96af1ab376eb"), Date = new DateTime(23, 4, 4), Information = "Информация 4", PersonProvidedId = Users[0].Id },
            new Data() { Id = new Guid("c9ef087d-059d-446f-a7e3-639c4f3523ee"), Date = new DateTime(23, 5, 5), Information = "Информация 5", PersonProvidedId = Users[1].Id },
            new Data() { Id = new Guid("0cc8cf83-0dde-4fa7-a140-0c95a47dc723"), Date = new DateTime(23, 6, 6), Information = "Информация 6", PersonProvidedId = Users[1].Id },
            new Data() { Id = new Guid("eeb851e3-61cc-410e-b572-12828858db5e"), Date = new DateTime(23, 7, 7), Information = "Информация 7", PersonProvidedId = Users[1].Id },
            new Data() { Id = new Guid("9e943e08-112b-46e1-b56a-70f27c625617"), Date = new DateTime(23, 8, 8), Information = "Информация 8", PersonProvidedId = Users[1].Id },
        };

        /// <summary>
        /// Проверки данных
        /// </summary>
        public static List<DataCheck> DataChecks { get; } = new List<DataCheck>()
        {
            new DataCheck { DataId = Datas[0].Id, UserId = Users[2].Id, Valid = true },
            new DataCheck { DataId = Datas[1].Id, UserId = Users[3].Id, Valid = false },
            new DataCheck { DataId = Datas[2].Id, UserId = Users[2].Id, Valid = null },
            new DataCheck { DataId = Datas[4].Id, UserId = Users[3].Id, Valid = true },
            new DataCheck { DataId = Datas[5].Id, UserId = Users[2].Id, Valid = false },
            new DataCheck { DataId = Datas[6].Id, UserId = Users[3].Id, Valid = null },
        };
    }
}
