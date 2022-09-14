package config

import (
	"fmt"

	"github.com/spf13/cobra"
)

// ConfigCmd represents the config command
var ConfigCmd = &cobra.Command{
	Use:   "config",
	Short: "commands for configuration",
	Long:  `commands for configuration`,
	Run: func(cmd *cobra.Command, args []string) {
		fmt.Println("config called")
	},
}

func init() {
	ConfigCmd.AddCommand(exportCmd)
}
