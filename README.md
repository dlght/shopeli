# 🛒 ShopELI – AWS Playground for Prescription-Based Daily Shopping

**ShopELI** is an AWS-powered experimental platform that transforms doctor’s prescriptions into personalized daily shopping lists.

Whether someone needs lactose-free milk, gluten-free bread, or specific supplements, ShopELI bridges healthcare and shopping to help users stay on track with their treatment or diet — automatically and intelligently.

---

## 💡 What ShopELI Does

1. 🧾 **Accepts a Doctor’s Prescription**
   - Users can upload a file or enter text.
   - Sample: _"Consume lactose-free milk daily. Avoid gluten. Eat 2 bananas per day."_

2. 🧠 **Processes and Understands the Text**
   - Extracts medically and nutritionally relevant items.
   - Applies filters like allergens and preferences.

3. 🛍️ **Builds a Smart Shopping List**
   - Matches prescription with available daily-use items.
   - Formats the output for display, email, or export.

4. 📤 **(Optional) Sends the List to a Delivery Provider**
   - Integration-ready for food/grocery APIs or services.

---

## 🗺️ AWS Playground Architecture Overview

ShopELI is built as a cloud-native solution on AWS with the following capabilities:

- Secure upload of prescriptions
- Text extraction from files (images or PDFs)
- Intelligent parsing and item matching
- Storage of prescriptions, shopping lists, and history
- (Optional) Web dashboard for managing prescriptions and lists
- Notification system for daily reminders or delivery updates

---

## 🔄 Example

**Input (Prescription):**
