package commands

import (
	"github.com/Cethien/dsc/data"
	"github.com/fatih/color"
	"github.com/rodaine/table"
	"github.com/spf13/cobra"
)

// listCmd represents the list command
var listCmd = &cobra.Command{
	Use:   "list",
	Short: "list all saved commands",
	Long:  `list all saved commands.`,
	Run: func(cmd *cobra.Command, args []string) {
		db, _ := data.OpenDatabase()

		headerFmt := color.New(color.FgGreen, color.Underline).SprintfFunc()
		columnFmt := color.New(color.FgYellow).SprintfFunc()

		var commands []data.Command
		rows, _ := db.Find(&commands).Rows()
		tbl := table.New("ID", "Command")
		tbl.WithHeaderFormatter(headerFmt).WithFirstColumnFormatter(columnFmt)

		for rows.Next() {
			var c data.Command
			db.ScanRows(rows, &c)
			tbl.AddRow(c.ID, c.CommandText)
		}
		tbl.Print()
	},
}

func init() {
}
