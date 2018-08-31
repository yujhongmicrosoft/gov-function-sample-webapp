import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ImagesComponent } from './images/images.component';
import { UploadComponent } from './upload/upload.component';
import { ImageComponent } from './image/image.component';

const routes: Routes = [
  { path: '', component: HomeComponent},
  { path: 'images', component: ImagesComponent},
  { path: 'images/:imageId', component: ImageComponent },
  { path: 'upload', component: UploadComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
