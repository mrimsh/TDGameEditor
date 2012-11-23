using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GameEditor.ContentInfoClasses
{
    public class MapsCollection
    {
        public List<MapInfo> maps = new List<MapInfo>();

        public MapsCollection() { }
    }

    public class MapInfo
    {
        private string name = "New Map";
        private string icon = "none";
        private int initialSeeds = 100;
        private float waveDelay = 5f;
        private List<Way> ways = new List<Way>();
        private List<Spawner> spawners = new List<Spawner>();
        private List<TowerPad> towerPads = new List<TowerPad>();
        //[Category("1. Идентификация")]

        [DisplayName("(Название)")]
        [Description("Название карты.")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DisplayName("Иконка")]
        [Description("Название иконки.")]
        public string Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        [DisplayName("Количество семян")]
        [Description("Количество семян, получаемое в начале уровня.")]
        public int InitialSeeds
        {
            get { return initialSeeds; }
            set { initialSeeds = value; }
        }

        [DisplayName("Задержка волны")]
        [Description("Время (в секундах), которое должно пройти после убийства последнего монстра, перед началом следующей волны.")]
        public float WaveDelay
        {
            get { return waveDelay; }
            set { waveDelay = value; }
        }

        [DisplayName("Пути")]
        [Description("Наборы вейпоинтов, по которым будут ходить мобы.")]
        public List<Way> Ways
        {
            get { return ways; }
            set { ways = value; }
        }

        [DisplayName("Спавнеры")]
        [Description("Точки из которых выходят враждебные мобы.")]
        public List<Spawner> Spawners
        {
            get { return spawners; }
            set { spawners = value; }
        }

        [DisplayName("Площадки")]
        [Description("Площадки, на которых можно строить башни.")]
        public List<TowerPad> TowerPads
        {
            get { return towerPads; }
            set { towerPads = value; }
        }

        public MapInfo()
        {
            ways.Add(new Way());
        }
    }

    public class Way
    {
        public string name = "New Way";

        [DisplayName("(Имя)")]
        [Description("Название путя.")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                if (form_mapEdit.Instance != null)
                {
                    int lastSelectedIndex = form_mapEdit.Instance.combobox_SelectedWay.SelectedIndex;
                    form_mapEdit.Instance.RefreshSelectedWayCombobox();
                    form_mapEdit.Instance.combobox_SelectedWay.SelectedIndex = lastSelectedIndex;
                }
            }
        }

        public List<WayPoint> waypoints = new List<WayPoint>();


        [DisplayName("Путевые точки (Waypoints)")]
        [Description("Координаты ключевых точек данного маршрута.")]
        public List<WayPoint> Waypoints
        {
            get
            {
                return waypoints;
            }
            set
            {
                waypoints = value;
            }
        }

        public Way() { }
    }

    public class WayPoint
    {
        private Point position = Point.Empty;

        [DisplayName("Позиция")]
        [Description("Координата точки на карте.")]
        public Point Position
        {
            get
            {
                return position;
            }
            set
            {
                if (position != value)
                {
                    position = value;
                    form_mapEdit.Instance.SetWaypointPosition(this, position);
                }
            }
        }

        public WayPoint() { }
    }

    public class Spawner
    {
        private Point position = Point.Empty;

        [DisplayName("Позиция")]
        [Description("Координата спавнера на карте.")]
        public Point Position
        {
            get { return position; }
            set { position = value; }
        }

        private List<Wave> waves = new List<Wave>();

        [DisplayName("Волны")]
        [Description("Волны мобов выходящих из данного спавнера.")]
        public List<Wave> Waves
        {
            get { return waves; }
            set { waves = value; }
        }
    }

    public class Wave
    {
        private List<WavePack> packs = new List<WavePack>();

        [DisplayName("Наборы мобов")]
        [Description("Наборы мобов одного типа выходящих в данной волне.")]
        public List<WavePack> Packs
        {
            get { return packs; }
            set { packs = value; }
        }

        public Wave() { }
    }

    public class WavePack
    {
        private string name;

        [DisplayName("Имя моба")]
        [Description("Имя моба, который нужно выпустить в этой волне.")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int count;

        [DisplayName("Количество")]
        [Description("Количество мобов указанного типа, которых необходимо выпустить в текующую волну.")]
        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        private string way;

        [DisplayName("Путь")]
        [Description("Количество мобов указанного типа, которых необходимо выпустить в текующую волну.")]
        [TypeConverter(typeof(WayTypeConverter))]
        public string Way
        {
            get { return way; }
            set { way = value; }
        }

        public WavePack() { }
    }

    public class TowerPad
    {
        private Point position = Point.Empty;

        [DisplayName("Позиция")]
        [Description("Координата площадки на карте.")]
        public Point Position
        {
            get { return position; }
            set { position = value; }
        }

        public TowerPad() { }
    }

    /// <summary>
    /// Конвертор типов, который позволяет задавать путь для каждого моба в спавнере, выбирая нужный путь из списка существующих путей.
    /// </summary>
    public class WayTypeConverter : StringConverter
    {
        // <summary>
        /// Будем предоставлять выбор из списка
        /// </summary>
        public override bool GetStandardValuesSupported(
          ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// ... и только из списка
        /// </summary>
        public override bool GetStandardValuesExclusive(
          ITypeDescriptorContext context)
        {
            // false - можно вводить вручную
            // true - только выбор из списка
            return true;
        }

        /// <summary>
        /// А вот и список
        /// </summary>
        public override StandardValuesCollection GetStandardValues(
          ITypeDescriptorContext context)
        {
            // возвращаем список строк из настроек программы
            // (базы данных, интернет и т.д.)
            return new StandardValuesCollection(form_mapEdit.Instance.GetWays());
        }
    }
}
