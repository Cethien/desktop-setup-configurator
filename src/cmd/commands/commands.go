package commands

import (
	"fmt"

	"github.com/spf13/cobra"
)

// CommandsCmd represents the commands command
var CommandsCmd = &cobra.Command{
	Use:   "commands",
	Short: "manage your Commands",
	Long:  ``,
	Run: func(cmd *cobra.Command, args []string) {
		fmt.Println("commands called")
	},
}

func init() {
	CommandsCmd.AddCommand(addCmd)
	CommandsCmd.AddCommand(modCmd)
	CommandsCmd.AddCommand(removeCmd)
	CommandsCmd.AddCommand(listCmd)
}
