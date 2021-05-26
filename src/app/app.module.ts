import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule }   from '@angular/forms';
import { AppComponent } from './app.component';
import { UserComponent } from './user/user.component';
import { UserFormComponent } from './user/user-form/user-form.component';
import { UserShowComponent } from './user/user-show/user-show.component';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { UserService } from './shared/user.service';
import { Guid } from "guid-typescript";

const itemRoutes: Routes = [
    { path: '', component: UserFormComponent},
    { path: 'show', component: UserShowComponent},
];
@NgModule({
  imports: [
    BrowserModule,FormsModule,HttpClientModule,ReactiveFormsModule,RouterModule.forRoot(itemRoutes)
  ],
  declarations: [
    AppComponent,
    UserComponent,
    UserFormComponent,
    UserShowComponent
  ],
  providers: [UserService],
  bootstrap: [AppComponent]
})
export class AppModule { }
