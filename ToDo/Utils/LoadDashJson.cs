using CefSharp.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using UIDisplay.Cards;
using UIDisplay.Pages;

namespace UIDisplay.Utils
{
    public class IgnoredCard
    {
        public static string[] enumsofType = new string[7] { "arxiv", "battery", "todo", "weather", "tomato", "weathersmall","tomatomedium" };
        public int width { get; set; }
        public int height { get; set; }
        public int row { get; set; }
        public int col { get; set; }
        public int typeNum { get; set; }
        public string type;
        public IgnoredCard() { }
        public IgnoredCard(Card card, int typenum)
        {
            this.width = card.stdWidth;
            this.height = card.stdHeight;
            this.row = card.row;
            this.col = card.colomn;
            this.typeNum = typenum;
            this.type = enumsofType[typenum];
        }
    }
    public class LoadDashJson
    {
        string filePath = "../../dashboard.json";
        List<IgnoredCard> dashboardCardList;
        public LoadDashJson()
        {
            dashboardCardList = new List<IgnoredCard>();
            //File.WriteAllText(filePath, string.Empty);
            RecoveryInitial();
        }
        public void AddCard(IgnoredCard card)
        {
            dashboardCardList.Add(card);
            string json = JsonConvert.SerializeObject(dashboardCardList);
            File.WriteAllText(filePath, json);
        }
        public void RemoveCard(IgnoredCard card)
        {
            dashboardCardList.Remove(card);
            string json = JsonConvert.SerializeObject(dashboardCardList);
            File.WriteAllText(filePath, json);
        }
        public void SetCardPosition(Grid grid, int row, int colomn, Card card)
        {
            Grid.SetRow(card, row);
            Grid.SetColumn(card, colomn);
            card.colomn = colomn;
            card.row = row;
            if ((int)card.Width == Constants.BIG_CARD_LENGTH)
                Grid.SetColumnSpan(card, 2);
            if ((int)card.Height == Constants.BIG_CARD_LENGTH)
                Grid.SetRowSpan(card, 2);
            grid.Children.Add(card);
        }

        public void RecoveryInitial()
        {
            string loadedJsonString = File.ReadAllText(filePath);
            List<IgnoredCard> loadedObjectList = JsonConvert.DeserializeObject<List<IgnoredCard>>(loadedJsonString);
            if (loadedObjectList == null) return;
            foreach (var loadedObject in loadedObjectList)
            {
                dashboardCardList.Add((IgnoredCard)loadedObject);
                //Console.WriteLine($"width: {loadedObject.width}, height: {loadedObject.height}, row: {loadedObject.row} , col: {loadedObject.col} , typeNum: {loadedObject.typeNum}");
            }
            foreach (var cardIgnored in dashboardCardList)
            {
                switch (cardIgnored.typeNum)
                {
                    case 0:
                        ArxivCard arxivCard = new ArxivCard();
                        SetCardPosition(Dashboard.inGrid, cardIgnored.row, cardIgnored.col, arxivCard);
                        break;
                    case 1:
                        BatteryCard batteryCardSmall = new BatteryCard();
                        SetCardPosition(Dashboard.inGrid, cardIgnored.row, cardIgnored.col, batteryCardSmall);
                        break;
                    case 2:
                        TodoCard todoCard = new TodoCard();
                        SetCardPosition(Dashboard.inGrid, cardIgnored.row, cardIgnored.col, todoCard);
                        Dashboard.AddNewTodoCard(todoCard);
                        break;
                    case 3:
                        WeatherCardBig weatherCardBig = new WeatherCardBig();
                        SetCardPosition(Dashboard.inGrid, cardIgnored.row, cardIgnored.col, weatherCardBig);
                        break;
                    case 4:
                        TomatoCard tomatoCard = new TomatoCard();
                        SetCardPosition(Dashboard.inGrid, cardIgnored.row, cardIgnored.col, tomatoCard);
                        break;
                    case 5:
                        WeatherCardSmall weatherCardSmall = new WeatherCardSmall();
                        SetCardPosition(Dashboard.inGrid, cardIgnored.row, cardIgnored.col, weatherCardSmall);
                        break;
                }
            }

        }
    }
}
