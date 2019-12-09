// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  protocol: 'https://',
  host: 'localhost:',
  port: '44306',
  auth: '/Auth',
  singUp: '/Register',
  singIn : '/Login',
  forgotPassword : '/ForgotPassword',
  resetPassword : '/ResetPasssword',
  refreshToken : '/RefreshToken',
  account: '/Account',
  author: '/Author',
  printingEdition: '/PrintingEdition',
  order: '/Order',
  getall: '/GetAll',
  getallwithoutremove: '/GetAllWithoutRemove',
  getByFullName: '/GetByFullName',
  pagination: '/Pagination',
  buy: '/Buy',
  sort: '/Sort',
  filtration: '/Filtration',
  createUser: '/CreateUser',
  create: '/Create',
  addImage: '/AddImage',
  deleteUser: '/DeleteUser',
  deleteImage:'/DeleteImage',
  finalRemovalUser: '/FinalRemovalUser',
  getAllRole : '/GetAllRoles',
  createRole: '/CreateRole',
  addingRoleUser: '/AddingRoleUser',
  takingRoleUser: '/TakingAwayUserRole',
  updateRole: '/UpdateRole',
  deleteRole: '/DeleteRole',

  message: [],
  accesstoken:''
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
