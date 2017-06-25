#region → Usings   .
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using System.ComponentModel;
using Telerik.Windows.Controls.GridView;
using System.Windows.Data;
using System.Linq;
#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 24.03.12   Yousra Reda         • creation
 * **********************************************
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion
namespace citPOINT.eNeg.Common
{
    /// <summary>
    /// Act a as a filter row for telerik grid
    /// </summary>
    public class CustomFilterRow
    {
        #region → Fields         .

        private RadGridView radGridView;
        private FilteringRow filteringRow;

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool),
                typeof(CustomFilterRow),
                new PropertyMetadata(new PropertyChangedCallback(OnIsEnabledPropertyChanged))
            );
        #endregion

        #region → Constractor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomFilterRow"/> class.
        /// </summary>
        /// <param name="radGridView">The RAD grid view.</param>
        public CustomFilterRow(RadGridView radGridView)
        {
            this.radGridView = radGridView;
            this.radGridView.IsFilteringAllowed = false;
            this.radGridView.DataLoaded += new EventHandler<EventArgs>(radGridView_DataLoaded);
        }
        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Handles the DataLoaded event of the radGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void radGridView_DataLoaded(object sender, EventArgs e)
        {
            this.filteringRow = new FilteringRow();
            this.filteringRow.Loaded += this.FilteringRow_Loaded;

            this.radGridView.RowLoaded += this.radGridView_RowLoaded;
            this.radGridView.ColumnDisplayIndexChanged += this.ReorderFilterCells;
            this.radGridView.Grouped += this.RecalculateIndentPresenterWidth;

            this.radGridView.DataLoaded -= this.radGridView_DataLoaded;
        }

        /// <summary>
        /// Handles the Loaded event of the FilteringRow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void FilteringRow_Loaded(object sender, RoutedEventArgs e)
        {
            GridViewScrollViewer gridViewScrollViewer = this.radGridView.ChildrenOfType<GridViewScrollViewer>().FirstOrDefault();
            gridViewScrollViewer.ScrollChanged += this.GridViewScrollViewer_ScrollChanged;

            this.BindToColumns();

            this.PlaceCommonHeaders();

            this.filteringRow.CustomFilterRowGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
        }

        /// <summary>
        /// Handles the RowLoaded event of the radGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Windows.Controls.GridView.RowLoadedEventArgs"/> instance containing the event data.</param>
        private void radGridView_RowLoaded(object sender, RowLoadedEventArgs e)
        {
            if (e.Row is GridViewHeaderRow)
            {
                Grid rootPanelGrid = (from c in this.radGridView.ChildrenOfType<Grid>()
                                      where c.Name == "PART_RootPanel"
                                      select c).FirstOrDefault();

                if (rootPanelGrid != null && this.filteringRow.Parent == null)
                {
                    rootPanelGrid.Loaded += this.rootPanelGrid_Loaded;
                }

                Border indicator = (from c in this.radGridView.ChildrenOfType<Border>()
                                    where c.Name == "PART_IndicatorPresenter"
                                    select c).FirstOrDefault();

                if (indicator != null)
                {
                    this.filteringRow.CustomFilterRowRoot.ColumnDefinitions[0].Width = GridLength.Auto;
                    Border indicatorFiller = new Border();
                    indicatorFiller.Name = "PART_IndicatorPresenterForFilterRow";
                    indicatorFiller.SetValue(Grid.ColumnProperty, 0);
                    this.filteringRow.CustomFilterRowRoot.Children.Add(indicatorFiller);

                    Binding widthBinding = new Binding("Width");
                    widthBinding.Source = indicator;
                    indicatorFiller.SetBinding(Border.WidthProperty, widthBinding);

                    Binding visibilityBinding = new Binding("Visibility");
                    visibilityBinding.Source = indicator;
                    indicatorFiller.SetBinding(Border.VisibilityProperty, visibilityBinding);
                }
            }
        }

        /// <summary>
        /// Handles the Loaded event of the rootPanelGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void rootPanelGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Grid rootPanelGrid = sender as Grid;
            if (this.filteringRow.Parent == null)
            {
                rootPanelGrid.Children.Add(this.filteringRow);
                this.filteringRow.SetValue(Grid.RowProperty, 1);
            }
        }

        /// <summary>
        /// Handles the Loaded event of the filterCell control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void filterCell_Loaded(object sender, RoutedEventArgs e)
        {
            ColumnFilterCell filterCell = sender as ColumnFilterCell;
            if (filterCell.Column.MinWidth < filterCell.MinWidth)
            {
                filterCell.Column.MinWidth = filterCell.MinWidth;
            }
            BindWidthAndVisibilityOnFilterCell(ref filterCell, filterCell.Column);
        }

        /// <summary>
        /// Handles the PropertyChanged event of the FilterCell control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void FilterCell_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ColumnFilterCell columnHeader = (ColumnFilterCell)sender;
            RadGridView radGridView = columnHeader.ParentOfType<RadGridView>();

            if (e.PropertyName == "IsFilterActive")
            {
                if (columnHeader.IsFilterActive)
                {
                    radGridView.FilterDescriptors.Add(columnHeader.FilterDescriptor);
                }
                else
                {
                    radGridView.FilterDescriptors.Remove(columnHeader.FilterDescriptor);
                }
            }
        }

        /// <summary>
        /// Handles the ScrollChanged event of the GridViewScrollViewer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Windows.Controls.GridView.ScrollChangedEventArgs"/> instance containing the event data.</param>
        private void GridViewScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            this.filteringRow.ScrollViewer.ScrollToHorizontalOffset(e.HorizontalOffset);
        }
        #endregion

        #region → Methods        .

        #region → Public         .
        /// <summary>
        /// Gets the is enabled.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static bool GetIsEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnabledProperty);
        }

        /// <summary>
        /// Sets the is enabled.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsEnabledProperty, value);
        }
        #endregion

        #region → Private        .

        /// <summary>
        /// Called when [is enabled property changed].
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnIsEnabledPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            RadGridView radGridView = dependencyObject as RadGridView;
            if (radGridView != null)
            {
                if ((bool)e.NewValue)
                {
                    CustomFilterRow row = new CustomFilterRow(radGridView);
                }
            }
        }

        /// <summary>
        /// Recalculates the width of the indent presenter.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Telerik.Windows.Controls.GridViewGroupedEventArgs"/> instance containing the event data.</param>
        private void RecalculateIndentPresenterWidth(object sender, GridViewGroupedEventArgs e)
        {
            double presenterWidth = this.filteringRow.CustomFilterRowRoot.ColumnDefinitions[0].Width.Value;
            this.filteringRow.CustomFilterRowRoot.ColumnDefinitions[1].Width = new GridLength(this.radGridView.GroupDescriptors.Count * presenterWidth);
        }

        /// <summary>
        /// Binds to columns.
        /// </summary>
        private void BindToColumns()
        {
            for (int i = 0; i < this.NumberOfColumnsInGridView(this.radGridView); i++)
            {
                ColumnDefinition columndefinition = new ColumnDefinition();
                columndefinition.Width = GridLength.Auto;
                this.filteringRow.CustomFilterRowGrid.ColumnDefinitions.Add(columndefinition);
            }
        }

        /// <summary>
        /// Places the common headers.
        /// </summary>
        private void PlaceCommonHeaders()
        {
            for (int i = 0; i < this.NumberOfColumnsInGridView(this.radGridView); i++)
            {
                ColumnFilterCell filterCell = null;
               
                if (this.radGridView.Columns[i] is GridViewDataColumn &&
                    (this.radGridView.Columns[i] as GridViewDataColumn).DataType != null)
                {

                    filterCell = ColumnFilterCellFactory.CreateColumnHeader((GridViewDataColumn)this.radGridView.Columns[i]);
                }   
                else
                {
                    filterCell = new ColumnFilterCell();
                }
                filterCell.Column = this.radGridView.Columns[i];
                filterCell.PropertyChanged += this.FilterCell_PropertyChanged;
                filterCell.SetValue(Grid.ColumnProperty, i);
                this.filteringRow.CustomFilterRowGrid.Children.Add(filterCell);
                filterCell.Loaded += this.filterCell_Loaded;

                if (this.radGridView.Columns[i] is GridViewDataColumn &&
                    (this.radGridView.Columns[i] as GridViewDataColumn).DataType != null)
                {
                    filterCell.IsFilterActive = true;
                }
            }

            ColumnFilterCell lastEmptyHeader = new ColumnFilterCell();
            lastEmptyHeader.SetValue(Grid.ColumnProperty, this.filteringRow.CustomFilterRowGrid.ColumnDefinitions.Count());
            this.filteringRow.CustomFilterRowGrid.Children.Add(lastEmptyHeader);
        }

        /// <summary>
        /// Binds the width and visibility on filter cell.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <param name="column">The column.</param>
        private static void BindWidthAndVisibilityOnFilterCell(ref ColumnFilterCell cell, GridViewColumn column)
        {
            Binding columnWidthBinding = new Binding("ActualWidth");
            columnWidthBinding.Source = column;
            Binding columnVisibilityBinding = new Binding("IsVisible");
            columnVisibilityBinding.Converter = new BooleanToVisibilityConverter();
            columnVisibilityBinding.Source = column;
            cell.SetBinding(ColumnFilterCell.VisibilityProperty, columnVisibilityBinding);
            cell.SetBinding(ColumnFilterCell.WidthProperty, columnWidthBinding);
        }

        /// <summary>
        /// Reorders the filter cells.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Telerik.Windows.Controls.GridViewColumnEventArgs"/> instance containing the event data.</param>
        private void ReorderFilterCells(object sender, GridViewColumnEventArgs e)
        {
            ColumnFilterCell firstCell = (from c in this.filteringRow.CustomFilterRowGrid.Children.OfType<ColumnFilterCell>()
                                          where c.Column == e.Column
                                          select c).FirstOrDefault();

            if (firstCell != null)
            {
                firstCell.SetValue(Grid.ColumnProperty, e.Column.DisplayIndex);
            }
        }

        /// <summary>
        /// Numbers the of columns in grid view.
        /// </summary>
        /// <param name="gridView">The grid view.</param>
        /// <returns></returns>
        private int NumberOfColumnsInGridView(RadGridView gridView)
        {
            int number = 0;
            foreach (GridViewColumn column in gridView.Columns)
            {
                number++;
            }
            return number;
        }

        #endregion
        #endregion
    }
}
