import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainLayoutComponent } from './main-layout/main-layout.component';

const routes: Routes = [
  { path: '', component : MainLayoutComponent, children:[ 
    { path: '', redirectTo:"/home", pathMatch: 'full'},
    { path: 'home', loadChildren: () => import('./printing-edition/printing-edition.module').then(mod =>mod.PrintingEditionModule ) },
    { path: 'auth', loadChildren: () => import('./auth/auth.module').then(mod =>mod.AuthModule ) },
    { path: 'user', loadChildren: () => import('./user/user.module').then(mod =>mod.UserModule ) }, 
 // { path: 'author', loadChildren: () => import('./author/author.module').then(mod =>mod.AuthorModule ) },
    ],
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],

})

export class AppRoutingModule { }
