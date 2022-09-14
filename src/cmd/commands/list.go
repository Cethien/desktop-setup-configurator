package commands

import (
	"fmt"

	"github.com/Cethien/dsc/data"
	"github.com/fatih/color"
	"github.com/rodaine/table"
	"github.com/spf13/cobra"
)

var (
	headerFmt = color.New(color.FgGreen, color.Underline).SprintfFunc()
	columnFmt = color.New(color.FgYellow).SprintfFunc()
)

// listCmd represents the list command
var listCmd = &cobra.Command{
	Use:   "list",
	Short: "list all saved commands",
	Long:  `list all saved commands.`,
	Run: func(cmd *cobra.Command, args []string) {
		db, _ := data.OpenDatabase()
		var commands []data.Command
		rows, _ := db.Find(&commands).Rows()

		tbl := table.New("ID", "Command")
		tbl.WithHeaderFormatter(headerFmt).WithFirstColumnFormatter(columnFmt)

		i := 0
		for rows.Next() {
			i++
			var c data.Command
			db.ScanRows(rows, &c)
			tbl.AddRow(c.ID, c.CommandText)
		}

		// print output
		if i == 0 {
			fmt.Println("no commands found")
		} else {
			tbl.Print()
		}
	},
}

func init() {

}
