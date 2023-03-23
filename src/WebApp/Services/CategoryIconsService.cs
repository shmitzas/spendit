namespace WebApp.Services
{
    public class CategoryIconsService
    {
        public async Task<string> GetIconByName(string name)
        {
            switch (name)
            {
                case "Housing":
                    return "fa-solid fa-house";
                case "Groceries":
                    return "fa-solid fa-basket-shopping";
                case "Transportation":
                    return "fa-solid fa-car";
                case "Health":
                    return "fa-solid fa-heart-pulse";
                case "Entertainment":
                    return "fa-solid fa-masks-theater";
                case "Savings/Investments":
                    return "fa-solid fa-sack-dollar";
                case "Debt Repayment":
                    return "fa-solid fa-circle-info";
                case "Clothing":
                    return "fa-solid fa-shirt";
                case "Gifts/Donations":
                    return "fa-solid fa-hand-holding-heart";
                case "Travel":
                    return "fa-solid fa-plane-departure";
                case "Education":
                    return "fa-solid fa-school-flag";
                case "Subscriptions/Memberships":
                    return "fa-solid fa-money-check-dollar";
                case "Childcare":
                    return "fa-solid fa-children";
                case "Pets":
                    return "fa-solid fa-paw";
                case "Miscellaneous/Other":
                    return "fa-solid fa-shuffle";
                default:
                    return "fa-solid fa-shuffle";
            }
        }
    }
}
