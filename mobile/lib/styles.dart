import 'package:flutter/material.dart';

class AppStyles {
  // Background image decoration
  static const BoxDecoration backgroundDecoration = BoxDecoration(
    image: DecorationImage(
      image: AssetImage('assets/background.png'),
      fit: BoxFit.cover,
    ),
  );

  // Semi-transparent filter
  static const Color filterColor = Colors.black87;

  // Semi-transparent white background
  static const Color transparentWhite = Colors.white70;

  // Header style
  static const TextStyle headerStyle = TextStyle(
    color: Color.fromARGB(255, 49, 49, 49),
    fontSize: 18,
    fontWeight: FontWeight.bold,
  );

  // General text style
  static const TextStyle textStyle = TextStyle(
    color: Colors.black,
    fontSize: 12,
  );

  // Input field (text field) decoration style
  static InputDecoration inputFieldStyle({
    required String hintText,
    bool isPassword = false,
  }) {
    return InputDecoration(
      hintText: hintText,
      hintStyle: const TextStyle(color: Colors.white54),
      filled: true,
      fillColor: Colors.transparent, // Transparent background for text fields
      contentPadding: const EdgeInsets.symmetric(vertical: 16, horizontal: 20),
      border: OutlineInputBorder(
        borderRadius: BorderRadius.circular(20.0), // Rounded corners
        borderSide: const BorderSide(color: Colors.white54), // Border color
      ),
      enabledBorder: OutlineInputBorder(
        borderRadius: BorderRadius.circular(20.0),
        borderSide: const BorderSide(color: Colors.white54),
      ),
      focusedBorder: OutlineInputBorder(
        borderRadius: BorderRadius.circular(20.0),
        borderSide: const BorderSide(color: Colors.white), // Solid white border when focused
      ),
    );
  }

  // Button style (e.g., for login or register button)
  static ButtonStyle buttonStyle() {
    return ElevatedButton.styleFrom(
      backgroundColor: Colors.transparent, // Transparent background
      foregroundColor: Colors.white, // White text color
      side: const BorderSide(color: Colors.white), // White border
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(20.0), // Rounded corners
      ),
      padding: const EdgeInsets.symmetric(horizontal: 40, vertical: 12),
    );
  }

  // TextButton style (for links or secondary actions like Cancel/Save)
  static ButtonStyle textButtonStyle() {
    return TextButton.styleFrom(
      foregroundColor: Colors.white, // Change text color to white
      textStyle: const TextStyle(
        fontSize: 14,
        fontWeight: FontWeight.w500,
      ),
    );
  }

  // Form title style (e.g., "Login" or "Register" title)
  static const TextStyle formTitleStyle = TextStyle(
    color: Colors.white,
    fontSize: 24,
    fontWeight: FontWeight.bold,
  );
}
