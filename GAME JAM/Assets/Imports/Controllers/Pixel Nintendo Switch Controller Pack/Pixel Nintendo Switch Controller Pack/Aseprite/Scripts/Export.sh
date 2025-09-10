#!/bin/bash

ASEPRITE_PATH="Aseprite"
FACE_BUTTONS_PATH="../FaceButtons.aseprite"
FACELESS_BUTTONS_PATH="../FacelessButtons.aseprite"
SHOULDER_BUTTONS_PATH="../ShoulderButtons.aseprite"
DIRECTIONAL_BUTTONS_PATH="../DirectionalButtons.aseprite"
SYSTEM_BUTTONS_PATH="../SystemButtons.aseprite"
ICONS_PATH="../Icons.aseprite"
INDICATORS_PATH="../Indicators.aseprite"
CONTROLLER_PATH="../Controller.aseprite"
CONTROLLER_DIAGRAMS_PATH="../ControllerDiagrams.aseprite"
TRACKPADS_PATH="../Trackpads.aseprite"
THUMBSTICKS_PATH="../Thumbsticks.aseprite"
SPLASH_PATH="../Splash.aseprite"
JOYCONS_PATH="../Joycons.aseprite"
LOGOS_PATH="../Logos.aseprite"
EXPORT_FOLDER_TAG_COMBINATIONS="./ExportFolderTagCombinations.lua"
EXPORT_TAGS="./ExportTags.lua"
EXPORT_SLICES="./ExportSlices.lua"
EXPORT_SHEET="./ExportSheet.lua"
SPRITES_FOLDER="../../Sprites/"
PARAMS="--script-param sprites-folder=$SPRITES_FOLDER"

display_menu() {
  echo "Please choose an option:"
  echo "1. Face Buttons"
  echo "2. Faceless Buttons"
  echo "3. Directional Buttons"
  echo "4. Shoulder Buttons"
  echo "5. System Buttons"
  echo "6. Joycons"
  echo "7. Thumbsticks"
  echo "8. Controller & Diagrams"
  echo "9. Indicators"
  echo "10. Icons, Logos & Splash Screens"
  echo "11. All"
  echo "12. Exit"
}

export_face_buttons() {
  echo "Exporting Face Buttons"
  "$ASEPRITE_PATH" -b "$FACE_BUTTONS_PATH" $PARAMS --script "$EXPORT_FOLDER_TAG_COMBINATIONS"
}

export_faceless_buttons() {
  echo "Exporting Faceless Buttons"
  "$ASEPRITE_PATH" -b "$FACELESS_BUTTONS_PATH" $PARAMS --script "$EXPORT_FOLDER_TAG_COMBINATIONS"
}

export_shoulder_buttons() {
  echo "Exporting Shoulder Buttons"
  "$ASEPRITE_PATH" -b "$SHOULDER_BUTTONS_PATH" $PARAMS --script "$EXPORT_FOLDER_TAG_COMBINATIONS"
}

export_directional_buttons() {
  echo "Exporting Directional Buttons"
      "$ASEPRITE_PATH" -b "$DIRECTIONAL_BUTTONS_PATH" $PARAMS --script "$EXPORT_FOLDER_TAG_COMBINATIONS"
}

export_system_buttons() {
  echo "Exporting System Buttons"
      "$ASEPRITE_PATH" -b "$SYSTEM_BUTTONS_PATH" $PARAMS --script "$EXPORT_FOLDER_TAG_COMBINATIONS"
}

export_joycons_buttons() {
  echo "Exporting Joycons"
      "$ASEPRITE_PATH" -b "$JOYCONS_PATH" $PARAMS --script "$EXPORT_FOLDER_TAG_COMBINATIONS"
}

export_thumbsticks() {
  echo "Exporting Thumbsticks"
      "$ASEPRITE_PATH" -b "$THUMBSTICKS_PATH" $PARAMS --script "$EXPORT_FOLDER_TAG_COMBINATIONS"
}

export_controller() {
  echo "Exporting Controller"
      "$ASEPRITE_PATH" -b "$CONTROLLER_PATH" $PARAMS --script-param trim-cels="true" --script "$EXPORT_SLICES"
}

export_controller_diagrams() {
  echo "Exporting Controller Diagrams"
      "$ASEPRITE_PATH" -b "$CONTROLLER_DIAGRAMS_PATH" $PARAMS --script-param trim-cels="true" --script "$EXPORT_FOLDER_TAG_COMBINATIONS"
}

export_indicators() {
  echo "Exporting Indicators"
      "$ASEPRITE_PATH" -b "$INDICATORS_PATH" $PARAMS --script "$EXPORT_FOLDER_TAG_COMBINATIONS"
}

export_icons() {
  echo "Exporting Icons"
      "$ASEPRITE_PATH" -b "$ICONS_PATH" $PARAMS --script "$EXPORT_SLICES"
      "$ASEPRITE_PATH" -b "$ICONS_PATH" $PARAMS --script "$EXPORT_SHEET"
}

export_splash_screen() {
  echo "Exporting Logos"
      "$ASEPRITE_PATH" -b "$LOGOS_PATH" $PARAMS --script "$EXPORT_SLICES"
      "$ASEPRITE_PATH" -b "$LOGOS_PATH" $PARAMS --script "$EXPORT_SHEET"
}

export_logos() {
  echo "Exporting Splash Screen"
      "$ASEPRITE_PATH" -b "$SPLASH_PATH" $PARAMS --script-param trim="false" --script "$EXPORT_FOLDER_TAG_COMBINATIONS"
}

while true; do
  display_menu
  read -p "Enter your choice [1-12]: " choice

  case $choice in
    1)
      export_face_buttons
      ;;
    2)
      export_faceless_buttons
      ;;
    3)
      export_directional_buttons
      ;;
    4)
      export_shoulder_buttons
      ;;
    5)
      export_system_buttons
      ;;
    6)
      export_joycons_buttons
      ;;
    7)
      export_thumbsticks
      ;;
    8)
      export_controller
      export_controller_diagrams
      ;;
    9)
      export_indicators
      ;;
    10)
      export_splash_screen
      export_icons
      export_logos
      ;;
    11)
      export_face_buttons
      export_faceless_buttons
      export_directional_buttons
      export_shoulder_buttons
      export_system_buttons
      export_joycons_buttons
      export_thumbsticks
      export_controller
      export_controller_diagrams
      export_indicators
      export_splash_screen
      export_icons
      export_logos
      ;;
    12)
      echo "Exiting..."
      break
      ;;
    *)
      echo "Invalid option. Please try again."
      ;;
  esac

  echo ""
done