import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { AppRoutingModule } from './app-routing.module';
import { ImagesComponent } from './images/images.component';
import { UploadComponent } from './upload/upload.component';
import { ModeratorApiService } from './services/moderator-api.service';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { LoadingModule } from 'ngx-loading';
import { ImageComponent } from './image/image.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ImagesComponent,
    UploadComponent,
    ImageComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    LoadingModule,
    AppRoutingModule,
    NgbModule.forRoot()
  ],
  providers: [
    ModeratorApiService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
