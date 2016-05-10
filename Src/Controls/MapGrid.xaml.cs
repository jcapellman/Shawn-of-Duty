﻿using System.Collections.Generic;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using SpaceTactics.Transports;

namespace SpaceTactics.Controls {
    public sealed partial class MapGrid : UserControl {
        public IEnumerable<Tileset> ItemSource {
            set { SetValue(ItemSourceProperty, value); }
            get { return (IEnumerable<Tileset>) GetValue(ItemSourceProperty); }
        }

        public static readonly DependencyProperty ItemSourceProperty = DependencyProperty.Register("ItemSource", typeof(IEnumerable<Tileset>), typeof(MapGrid), null);

        public int Columns {
            get { return (int) GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        public static readonly DependencyProperty ColumnsProperty = DependencyProperty.Register("Columns", typeof(int), typeof(MapGrid), null);

        public int Rows {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        public static readonly DependencyProperty RowsProperty = DependencyProperty.Register("Rows", typeof(int), typeof(MapGrid), null);

        public MapGrid() {
            this.InitializeComponent();
            this.Loaded += MapGrid_Loaded;
        }

        private void MapGrid_Loaded(object sender, RoutedEventArgs e) {
            for (var x = 0; x < Columns; x++) {
                gMain.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }

            for (var x = 0; x < Rows; x++) {
                gMain.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            }

            var rowCount = 0;
            var currentRow = 0;

            foreach (var item in ItemSource) {
                if (rowCount == Columns) {
                    currentRow++;
                    rowCount = 0;
                }

                var tile = new MapTile(item);
                
                tile.SetValue(Grid.RowProperty, currentRow);
                tile.SetValue(Grid.ColumnProperty, rowCount);

                gMain.Children.Add(tile);

                rowCount++;
            }
        }
    }
}