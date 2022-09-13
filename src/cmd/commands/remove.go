package commands

import (
	"github.com/Cethien/dsc/data"
	"github.com/spf13/cobra"
)

// removeCmd represents the remove command
var removeCmd = &cobra.Command{
	Use:   "remove",
	Short: "remove a command from collection",
	Long:  `remove a command from collection`,
	Run: func(cmd *cobra.Command, args []string) {
		id := args[0]

		var c data.Command
		db, _ := data.OpenDatabase()
		db.Delete(&c, id)
	},
}

func init() {

}
