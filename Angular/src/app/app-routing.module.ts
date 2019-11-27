import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
    { path: '', loadChildren: () => import('./home/home.module').then(mod =>mod.HomeModule ), children : [
      { path: 'auth', loadChildren: () => import('./auth/auth.module').then(mod =>mod.AuthModule ) },
      { path: 'user', loadChildren: () => import('./user/user.component').then(mod =>mod.UserComponent ) }, 
      { path: 'author', loadChildren: () => import('./author/author.component').then(mod =>mod.AuthorComponent ) },
      { path: 'printingEdition', loadChildren: () => import('./printingEdition/printingEdition.component').then(mod =>mod.PrintingEditionComponent ) },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],

})

export class AppRoutingModule { }
