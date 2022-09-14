/*
Copyright © 2022 Borislaw Sotnikow borislaw.sotnikow@gmx.de
*/
package cmd

import (
	"os"

	"github.com/Cethien/dsc/cmd/commands"
	"github.com/Cethien/dsc/cmd/config"

	"github.com/spf13/cobra"
)

// rootCmd represents the base command when called without any subcommands
var rootCmd = &cobra.Command{
	Use:   "dsc",
	Short: "Desktop Setup Configurator",
	Long: `DSC is a tool an autistic guy wrote for reasons,
	
	It can: 
	- manage your install / download commands
	- manage profiles
	- manage you post install commands
	- generate an config file to use with powershell / cmd / bash`,
	// Uncomment the following line if your bare application
	// has an action associated with it:
	// Run: func(cmd *cobra.Command, args []string) { },
}

// Execute adds all child commands to the root command and sets flags appropriately.
// This is called by main.main(). It only needs to happen once to the rootCmd.
func Execute() {
	err := rootCmd.Execute()
	if err != nil {
		os.Exit(1)
	}
}

func addSubcommandPalettes() {
	rootCmd.AddCommand(commands.CommandsCmd)
	rootCmd.AddCommand(config.ConfigCmd)
}

func init() {
	// Here you will define your flags and configuration settings.
	// Cobra supports persistent flags, which, if defined here,
	// will be global for your application.

	// rootCmd.PersistentFlags().StringVar(&cfgFile, "config", "", "config file (default is $HOME/.dsc.yaml)")

	// Cobra also supports local flags, which will only run
	// when this action is called directly.
	rootCmd.Flags().BoolP("toggle", "t", false, "Help message for toggle")

	addSubcommandPalettes()
}
