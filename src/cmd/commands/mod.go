package commands

import (
	"github.com/Cethien/dsc/data"
	"github.com/spf13/cobra"
)

var (
	id      string
	cmdText string
)

// modCmd represents the mod command
var modCmd = &cobra.Command{
	Use:   "mod",
	Short: "modify a saved command",
	Long:  `modify a saved command`,
	Run: func(cmd *cobra.Command, args []string) {
		id = args[0]
		cmdText = args[1]

		db, _ := data.OpenDatabase()
		var c data.Command
		db.First(&c, id)
		c.CommandText = cmdText
		db.Save(&c)
	},
}

func init() {

}
