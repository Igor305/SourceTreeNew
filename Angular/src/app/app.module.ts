import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Interceptor } from './interceptor';

import { AppRoutingModule } from './app-routing.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSelectModule } from '@angular/material/select';
import { HeaderModule } from './header/header.module';
import { UserModule } from './user/user.module';
import { AuthModule } from './auth/auth.module';
import { PrintingEditionModule } from './printing-edition/printing-edition.module';

import { AppComponent } from './app.component';
import { AuthorComponent } from './author/author.component';
import { MainLayoutComponent } from './main-layout/main-layout.component';

import { AuthorService } from './services';
import { AuthService  } from './services/auth.service';
import { PrintingEditionService } from './services/printingEdition.service';
import { UserService } from './services/user.service';
import { RoleService } from './services/role.service';


@NgModule({
  declarations: [AppComponent, AuthorComponent, MainLayoutComponent],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    HttpClientModule,
    MatSelectModule,
    HeaderModule,
    UserModule,
    AuthModule,
    PrintingEditionModule,
    BrowserAnimationsModule
  ],
  providers: [
    AuthorService,
    PrintingEditionService,
    UserService,
    RoleService,
    AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: Interceptor,
      multi: true
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
