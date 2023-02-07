class User {
  id: number = 0;
  firstName: string='';
  lastName: string='';
  emailAddress: string='';
  password: string='';
  registrationDate: Date = new Date();


  registerUser() {
    return { Name: this.firstName + this.lastName, Password: this.password, RegistrationDate: this.registrationDate, isActive: true }
  }
}
export default User;
